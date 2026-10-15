# Default.FileStorage  Проект Default.FileStorage в составе Default.  ## Назначение - Категория: Infrastructure Module   ## Scope - Реализует ответственность своего слоя. - Не должен нарушать границы слоев.  ## Integration - ProjectReference: Default.Abstractions, Default.DependencyInjection - Consumers: Default.Api   ## DI / Config - DI files: S3ServiceCollectionExtensions.cs - Config sections: S3ConfigSection.cs   ## Key Files
- FileStorageService.cs
- IFileStorageService.cs
- S3ConfigSection.cs
- S3ServiceCollectionExtensions.cs

См. AGENT_CONTEXT.md и DEPENDENCY.md.
