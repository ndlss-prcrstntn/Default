# Default.DataAccess  Проект Default.DataAccess в составе Default.  ## Назначение - Категория: Infrastructure Module   ## Scope - Реализует ответственность своего слоя. - Не должен нарушать границы слоев.  ## Integration - ProjectReference: Default.DependencyInjection - Consumers: Default.Api, Default.DataAccess.EntityFramework, Default.DataAccess.Models, Default.Scheduler   ## DI / Config - DI files: DataAccessServiceCollectionExtensions.cs - Config sections: DataAccessConfigSection.cs   ## Key Files
- Abstractions\AudiableProperty.cs
- Abstractions\Entity.cs
- Abstractions\IUnitOfWork.cs
- Config\DataAccessConfigSection.cs
- DataAccessServiceCollectionExtensions.cs
- EntityTypeBuilderExtensions.cs
- EntityTypeConfiguration.cs
- MigratorHostExtensions.cs
- ServiceDbContext.cs
- UnitOfWork.cs

См. AGENT_CONTEXT.md и DEPENDENCY.md.
