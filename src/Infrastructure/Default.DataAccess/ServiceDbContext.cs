using Default.DataAccess.Config;
using Default.DependencyInjection;
using Default.Syntax.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;

namespace Default.DataAccess.EntityFramework;

public abstract class ServiceDbContext(DbContextOptions options, IOptions<DataAccessConfigSection> config)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasCollation("ru-RU-x-icu", "ru-RU-x-icu", "icu", false);

        modelBuilder.HasDefaultSchema(config.Value.Schema);
        modelBuilder.ApplyConfigurationsFromRootAssembly(Database);
    }
}

public static class ModelBuilderExtensions
{
    public static void ApplyConfigurationsFromRootAssembly(this ModelBuilder modelBuilder, DatabaseFacade database)
    {
        var applyEntityConfigurationMethod = typeof(ModelBuilder)
            .GetMethods()
            .Where(mi => mi.Name == nameof(ModelBuilder.ApplyConfiguration))
            .Where(mi => mi.ContainsGenericParameters)
            .Single(mi => mi.GetParameters().SingleOrDefault()?.ParameterType.GetGenericTypeDefinition() ==
                          typeof(IEntityTypeConfiguration<>));

        foreach (var type in AssemblyRoot.GetRootAssembly().GetLoadableTypesWithReferenced().OrderBy(t => t.FullName))
        {
            if (type.GetConstructor(Type.EmptyTypes) == null) continue;

            foreach (var @interface in type.GetInterfaces())
            {
                if (!@interface.IsGenericType) continue;

                if (@interface.GetGenericTypeDefinition() != typeof(IEntityTypeConfiguration<>)) continue;

                var target = applyEntityConfigurationMethod.MakeGenericMethod(@interface.GenericTypeArguments[0]);
                var instance = Activator.CreateInstance(type);

                if (instance is EntityTypeConfiguration configuration)
                    configuration.Database = database;

                target.Invoke(modelBuilder, [instance]);
            }
        }
    }
}