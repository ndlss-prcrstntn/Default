using System.Linq.Expressions;
using Default.DataAccess.Abstractions;

namespace Default.DataAccess.EntityFramework.Abstractions;

public interface IUnitOfWork
{
    void Add<TEntity>(TEntity domainObject) where TEntity : Entity;

    void Remove<TEntity>(TEntity domainObject) where TEntity : Entity;

    void Remove<TEntity>(IEnumerable<TEntity> domainObjects) where TEntity : Entity;

    IQueryable<TEntity> Query<TEntity>(bool withTracking = true) where TEntity : Entity;

    Task<ICollection<TEntity>> UpsertManyAsync<TEntity>(
        IEnumerable<TEntity> entities,
        Expression<Func<TEntity, object>> matchExpression,
        Expression<Func<TEntity, TEntity>> updateExpression,
        CancellationToken cancellationToken = default)
        where TEntity : Entity;

    Task<ICollection<TEntity>> UpsertManyAsync<TEntity>(
        IEnumerable<TEntity> entities,
        Expression<Func<TEntity, object>> matchExpression,
        CancellationToken cancellationToken = default)
        where TEntity : Entity;

    Task<TEntity> UpsertAsync<TEntity>(TEntity entity,
        Expression<Func<TEntity, object>> matchExpression,
        CancellationToken cancellationToken = default)
        where TEntity : Entity;

    Task<TEntity> UpsertAsync<TEntity>(TEntity entity,
        Expression<Func<TEntity, object>> matchExpression,
        Expression<Func<TEntity, TEntity>> updateExpression,
        CancellationToken cancellationToken = default) where TEntity : Entity;

    Task<ICollection<TEntity>> UpsertManyAsync<TEntity>(
        IEnumerable<TEntity> entities,
        Expression<Func<TEntity, object>> matchExpression,
        Expression<Func<TEntity, TEntity, TEntity>>? whenMatchedExpression = null,
        CancellationToken cancellationToken = default)
        where TEntity : Entity;


    bool IsChanged<TEntity>(TEntity domainObject) where TEntity : Entity;

    void Update<TEntity>(TEntity domainObject) where TEntity : Entity;

    Task CommitAsync(CancellationToken cancellationToken = default);

    void Clear();
}