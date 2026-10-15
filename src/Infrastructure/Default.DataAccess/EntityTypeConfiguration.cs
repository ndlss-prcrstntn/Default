using System.Collections;
using System.ComponentModel.DataAnnotations;
using Default.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Default.DataAccess.EntityFramework;

public abstract class EntityTypeConfiguration
{
    public DatabaseFacade Database { get; set; }
}

public abstract class EntityTypeConfiguration<TEntity> : EntityTypeConfiguration, IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureEntity(builder);

        var entityType = builder.Metadata;

        foreach (var property in entityType.GetProperties().ToArray())
        {
            ConfigurePropertyType(builder, property);
            ConfigureKeyNamingConvention(builder, entityType, property);
            ConfigureStringLength(property);
        }
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

    private void ConfigurePropertyType(EntityTypeBuilder<TEntity> builder, IMutableProperty property)
    {
        var clrType = property.ClrType;

        if (clrType == typeof(Guid) || clrType == typeof(Guid?))
            if (string.IsNullOrEmpty(property.GetColumnType()))
                builder.Property(clrType, property.Name).HasColumnType("uuid");

        if (clrType == typeof(decimal) || clrType == typeof(decimal?))
            if (string.IsNullOrEmpty(property.GetColumnType()))
                builder.Property(clrType, property.Name).HasColumnType("money");

        if (clrType == typeof(string)) builder.Property(clrType, property.Name).UseCollation("ru-RU-x-icu");

        if (clrType == typeof(DateOnly) || clrType == typeof(DateOnly?))
            if (string.IsNullOrEmpty(property.GetColumnType()))
                builder.Property(clrType, property.Name).HasColumnType("date");

        ConfigureDateTimeConversion(builder, property, clrType);
    }

    private void ConfigureDateTimeConversion(EntityTypeBuilder<TEntity> builder, IMutableProperty property,
        Type clrType)
    {
        if (clrType == typeof(DateTime))
            builder.Property<DateTime>(property.Name)
                .HasConversion(v => v.ToUniversalTime(), v => v.ToLocalTime());

        if (clrType == typeof(DateTime?))
            builder.Property<DateTime?>(property.Name)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,
                    v => v.HasValue ? v.Value.ToLocalTime() : null);

        if (clrType == typeof(DateTimeOffset))
            builder.Property<DateTimeOffset>(property.Name)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => v
                );

        if (clrType == typeof(DateTimeOffset?))
            builder.Property<DateTimeOffset?>(property.Name)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToUniversalTime() : (DateTimeOffset?)null,
                    v => v
                );
    }

    private void ConfigureKeyNamingConvention(EntityTypeBuilder<TEntity> builder, IMutableEntityType entityType,
        IMutableProperty property)
    {
        var clrType = property.ClrType;

        if (property.IsKey() && clrType == typeof(int) && !entityType.ClrType.IsAssignableTo(typeof(IEnumerable)))
        {
            var tableName = property.DeclaringType?.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
                builder.Property(clrType, property.Name).HasColumnName($"{tableName}Id");
        }
    }

    private void ConfigureStringLength(IMutableProperty property)
    {
        // Do not overwrite MaxLength already set via HasMaxLength() in ConfigureEntity.
        if (property.ClrType == typeof(string)
            && property.GetMaxLength() == null
            && property.PropertyInfo?.GetCustomAttributes(typeof(MaxLengthAttribute), true).Any() == false)
            property.SetMaxLength(512);
    }
}