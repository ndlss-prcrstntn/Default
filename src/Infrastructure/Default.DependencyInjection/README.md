# Default.DependencyInjection  Проект Default.DependencyInjection в составе Default.  ## Назначение - Категория: Infrastructure Module   ## Scope - Реализует ответственность своего слоя. - Не должен нарушать границы слоев.  ## Integration - ProjectReference: Default.Abstractions, Default.Syntax - Consumers: Default.Cache, Default.DataAccess, Default.FileStorage, Default.Logs, Default.Mail   ## DI / Config - DI files: InterceptorServiceCollectionExtensions.cs - Config sections: None   ## Key Files
- AssemblyExtensions.cs
- ConfigurationExtensions.cs
- InterceptorServiceCollectionExtensions.cs
- ServicesCollectionDescriptorExtensions.cs

См. AGENT_CONTEXT.md и DEPENDENCY.md.
