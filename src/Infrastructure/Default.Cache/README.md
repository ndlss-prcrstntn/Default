# Default.Cache  Проект Default.Cache в составе Default.  ## Назначение - Категория: Infrastructure Module   ## Scope - Реализует ответственность своего слоя. - Не должен нарушать границы слоев.  ## Integration - ProjectReference: Default.Abstractions, Default.DependencyInjection - Consumers: Default.Web   ## DI / Config - DI files: KeyDbServiceCollectionExtensions.cs - Config sections: KeyDbConfigSection.cs   ## Key Files
- Abstractions\ICacheStorage.cs
- Abstractions\ICacheStorageKey.cs
- CacheStorage.cs
- CacheStorageKey.cs
- KeyDbConfigSection.cs
- KeyDbConnectionFactory.cs
- KeyDbServiceCollectionExtensions.cs

См. AGENT_CONTEXT.md и DEPENDENCY.md.
