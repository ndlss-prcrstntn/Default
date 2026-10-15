# Default.Abstractions  Проект Default.Abstractions в составе Default.  ## Назначение - Категория: Infrastructure Module   ## Scope - Реализует ответственность своего слоя. - Не должен нарушать границы слоев.  ## Integration - ProjectReference: None - Consumers: Default.Cache, Default.DependencyInjection, Default.Ext, Default.FileStorage, Default.Hosting, Default.Mail, Default.Scheduler   ## DI / Config - DI files: None - Config sections: IConfigSection.cs   ## Key Files
- Exceptions\ClientException.cs
- Exceptions\PermissionDeniedException.cs
- Exceptions\ServiceException.cs
- Exceptions\UnauthorizedException.cs
- IConfigSection.cs
- ITraceEntity.cs
- IVersionExtractor.cs

См. AGENT_CONTEXT.md и DEPENDENCY.md.
