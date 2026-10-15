using System.Linq.Expressions;
using Default.DataAccess.Abstractions;
using Default.DataAccess.EntityFramework;
using Default.DataAccess.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Default.DataAccess;

public class UnitOfWork(ServiceDbContext context) : IUnitOfWork
{
    public void Add<TEntity>(TEntity domainObject) where TEntity : Entity => context.Add(domainObject);

    public void Remove<TEntity>(TEntity domainObject) where TEntity : Entity => context.Remove(domainObject);

    public void Remove<TEntity>(IEnumerable<TEntity> domainObjects) where TEntity : Entity
        => context.RemoveRange(domainObjects);

    public bool IsChanged<TEntity>(TEntity domainObject) where TEntity : Entity
        => context.ChangeTracker.Entries<TEntity>().Where(ee => ee.Entity == domainObject)
            .Any(ee => ee.State != EntityState.Unchanged);

    public virtual async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        const int maxAttempts = 10;

        bool saveFailed;
        var currentAttempt = 0;
        do
        {
            saveFailed = false;

            try
            {
                await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine("DbUpdateConcurrencyException");
                saveFailed = true;
                ResolveConflicts(e);
                currentAttempt += 1;
                Thread.Sleep(100);
            }
        } while (saveFailed && currentAttempt < maxAttempts);
    }

    public void Clear() => context.ChangeTracker.Clear();

    public IQueryable<TEntity> Query<TEntity>(bool withTracking = true) where TEntity : Entity
        => withTracking ? context.Set<TEntity>() : context.Set<TEntity>().AsNoTracking();

    public Task<ICollection<TEntity>> UpsertManyAsync<TEntity>(
        IEnumerable<TEntity> entities,
        Expression<Func<TEntity, object>> matchExpression,
        Expression<Func<TEntity, TEntity>> updateExpression,
        CancellationToken cancellationToken = default)
        where TEntity : Entity
        => context.Set<TEntity>()
            .UpsertRange(entities)
            .On(matchExpression)
            .WhenMatched(updateExpression)
            .RunAndReturnAsync();

    public async Task<ICollection<TEntity>> UpsertManyAsync<TEntity>(
        IEnumerable<TEntity> entities,
        Expression<Func<TEntity, object>> matchExpression,
        Expression<Func<TEntity, TEntity, TEntity>>? whenMatchedExpression = null,
        CancellationToken cancellationToken = default)
        where TEntity : Entity
    {
        var upsert = context.Set<TEntity>()
            .UpsertRange(entities)
            .On(matchExpression);

        if (whenMatchedExpression != null)
            upsert = upsert.WhenMatched(whenMatchedExpression);
        else
            upsert = upsert.NoUpdate();

        return await upsert.RunAndReturnAsync();
    }

    public async Task<TEntity> UpsertAsync<TEntity>(TEntity entity,
        Expression<Func<TEntity, object>> matchExpression,
        CancellationToken cancellationToken = default) where TEntity : Entity
    {
        var result = await context.Set<TEntity>()
            .Upsert(entity)
            .On(matchExpression)
            .NoUpdate()
            .RunAndReturnAsync();

        return result.FirstOrDefault();
    }

    public async Task<TEntity> UpsertAsync<TEntity>(TEntity entity,
        Expression<Func<TEntity, object>> matchExpression,
        Expression<Func<TEntity, TEntity>> updateExpression,
        CancellationToken cancellationToken = default) where TEntity : Entity
    {
        var result = await context.Set<TEntity>()
            .Upsert(entity)
            .On(matchExpression)
            .WhenMatched(updateExpression)
            .RunAndReturnAsync();

        return result.First();
    }

    public void Update<TEntity>(TEntity domainObject) where TEntity : Entity
        => context.Entry(domainObject).State = EntityState.Modified;

    public Task<ICollection<TEntity>> UpsertManyAsync<TEntity>(
        IEnumerable<TEntity> entities,
        Expression<Func<TEntity, object>> matchExpression,
        CancellationToken cancellationToken = default)
        where TEntity : Entity
        => context.Set<TEntity>()
            .UpsertRange(entities)
            .On(matchExpression)
            .NoUpdate()
            .RunAndReturnAsync();

    private static void ResolveConflicts(DbUpdateException e)
    {
        var entry = e.Entries.Single();
        var databaseValues = entry.GetDatabaseValues();
        var currentValues = entry.CurrentValues;
        var originalValues = entry.OriginalValues;

        if (originalValues == null) throw e;

        entry.OriginalValues.SetValues(databaseValues);

        var changedDatabaseProperties = databaseValues.Properties
            .Where(prop => !Equals(originalValues[prop.Name], databaseValues[prop.Name])).ToList();

        var changedClientProperties = currentValues.Properties
            .Where(prop => !Equals(originalValues[prop.Name], currentValues[prop.Name])).ToList();

        if (changedDatabaseProperties.Any(dbProp =>
                changedClientProperties.Any(cp =>
                    cp.Name.Equals(dbProp.Name) && !Equals(databaseValues[dbProp.Name], currentValues[cp.Name]))))
            throw new Exception("The data has already been changed. Try again");

        foreach (var property in changedDatabaseProperties)
            entry.Property(property.Name).IsModified = false;
    }

    public static async Task<Guid?> GetLinkedFieldValueByUidAsync(
        DbContext context,
        Type rootEntityType,
        Guid rootUid,
        LambdaExpression fieldSelector)
    {
        var uidProperty = rootEntityType.GetProperty("Uid")
                          ?? throw new InvalidOperationException(
                              $"Сущность {rootEntityType.Name} не содержит свойства 'Uid'.");

        var parameter = Expression.Parameter(rootEntityType, "e");
        var property = Expression.Property(parameter, uidProperty);
        var condition = Expression.Equal(property, Expression.Constant(rootUid));
        var whereLambda = Expression.Lambda(condition, parameter);

        var setMethod = typeof(DbContext)
            .GetMethod(nameof(DbContext.Set), Type.EmptyTypes)!
            .MakeGenericMethod(rootEntityType);
        var set = (IQueryable)setMethod.Invoke(context, null)!;

        var selectLambda = fieldSelector;

        var queryableType = typeof(Queryable);
        var whereMethod = queryableType.GetMethods()
            .First(m => m.Name == nameof(Queryable.Where)
                        && m.GetParameters().Length == 2)
            .MakeGenericMethod(rootEntityType);

        var filtered = whereMethod.Invoke(null, new object[] { set, whereLambda });

        var fieldType = fieldSelector.ReturnType;
        var selectMethod = queryableType.GetMethods()
            .First(m => m.Name == nameof(Queryable.Select)
                        && m.GetParameters().Length == 2)
            .MakeGenericMethod(rootEntityType, fieldType);

        var projected = selectMethod.Invoke(null, new[] { filtered, selectLambda });

        var firstMethod = typeof(EntityFrameworkQueryableExtensions)
            .GetMethods()
            .First(m => m.Name == nameof(EntityFrameworkQueryableExtensions.FirstOrDefaultAsync)
                        && m.GetParameters().Length == 1)
            .MakeGenericMethod(fieldType);

        var task = (Task)firstMethod.Invoke(null, new[] { projected })!;
        await task.ConfigureAwait(false);

        return (Guid?)task.GetType().GetProperty("Result")!.GetValue(task);
    }
}