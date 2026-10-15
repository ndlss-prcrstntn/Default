using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Default.DataAccess.EntityFramework;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder)
        where TEntity : class => entityTypeBuilder.ToTable(typeof(TEntity).Name);

    public static EntityTypeBuilder<TEntity> ToTableWithName<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder,
        string tableName)
        where TEntity : class => entityTypeBuilder.ToTable(tableName);
}