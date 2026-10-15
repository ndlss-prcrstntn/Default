# Default.Logs  Проект Default.Logs в составе Default.  ## Назначение - Категория: Infrastructure Module   ## Scope - Реализует ответственность своего слоя. - Не должен нарушать границы слоев.  ## Integration - ProjectReference: Default.DependencyInjection, Default.Hosting - Consumers: Default.Api, Default.Scheduler   ## DI / Config - DI files: LoggerServiceCollectionExtension.cs, MetricsServiceCollectionExtension.cs, ObservabilityHealthChecksExtensions.cs - Config sections: LogConfigSection; health checks читают `dataAccess:connection`, `keyDB:connection`, `s3:endpoint`   ## Key Files
- Config\LogConfigSecition.cs
- ISerilogSinkConfigurator.cs
- LoggerServiceCollectionExtension.cs
- LoggingApplicationBuilderExtensions.cs
- LoggingInterceptor.cs
- MetricsServiceCollectionExtension.cs
- ObservabilityApplicationBuilderExtensions.cs
- ObservabilityHealthChecksExtensions.cs
- SensitiveAttribute.cs
- TracedObjectDestructuringPolicy.cs

См. AGENT_CONTEXT.md и DEPENDENCY.md.
