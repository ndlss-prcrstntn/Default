# Default.Hosting  Проект Default.Hosting в составе Default.  ## Назначение - Категория: Infrastructure Module   ## Scope - Реализует ответственность своего слоя. - Не должен нарушать границы слоев.  ## Integration - ProjectReference: Default.Abstractions, Default.Syntax - Consumers: Default.Logs, Default.Scheduler, Default.Security   ## DI / Config - DI files: StartupTasks\StartupTaskServiceCollectionExtensions.cs - Config sections: None   ## Key Files
- AssemblyVersionExtractor.cs
- EnvironmentExtensions.cs
- EnvironmentNames.cs
- HostApplicationBuilderExtensions.cs
- StartupTasks\Filter\StartupFilter.cs
- StartupTasks\IStartupTask.cs
- StartupTasks\StartupTaskApplicationBuilderExtensions.cs
- StartupTasks\StartupTaskServiceCollectionExtensions.cs

См. AGENT_CONTEXT.md и DEPENDENCY.md.
