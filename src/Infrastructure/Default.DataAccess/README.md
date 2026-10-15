# Default.DataAccess

EF Core data access primitives: `DbContext` base types, unit of work, migrations host helpers, and configuration binding.

## Scope

- Persistence infrastructure shared by the host and scheduler.
- Domain entities belong in modules or dedicated model projects — not ad hoc in the host.

## Integration

- **Project references:** `Default.DependencyInjection`
- **Consumers:** `Default.Api`, `Default.DataAccess.EntityFramework`, `Default.DataAccess.Models`, `Default.Scheduler`

## DI / configuration

- **DI extensions:** `DataAccessServiceCollectionExtensions.cs`
- **Config sections:** `DataAccessConfigSection.cs`

## Key files

- `Abstractions\AudiableProperty.cs`
- `Abstractions\Entity.cs`
- `Abstractions\IUnitOfWork.cs`
- `Config\DataAccessConfigSection.cs`
- `DataAccessServiceCollectionExtensions.cs`
- `EntityTypeBuilderExtensions.cs`
- `EntityTypeConfiguration.cs`
- `MigratorHostExtensions.cs`
- `ServiceDbContext.cs`
- `UnitOfWork.cs`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
