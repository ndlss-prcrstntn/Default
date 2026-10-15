# Default.Scheduler  Проект Default.Scheduler в составе Default.  ## Назначение - Категория: Infrastructure Module   ## Scope - Реализует ответственность своего слоя. - Не должен нарушать границы слоев.  ## Integration - ProjectReference: Default.DataAccess, Default.Hosting - Consumers: None   ## DI / Config - DI files: None - Config sections: None   ## Key Files
- Abstractions\BaseRecurringJob.cs
- Abstractions\IJobService.cs
- Abstractions\IRecurringJob.cs
- DependencyInjection\HangfireServiceCollectionExtensions.cs
- DependencyInjection\RecurringJobsStartupTask.cs
- JobDescriptor.cs
- JobService.cs
- Model\JobInfo.cs
- Model\JobState.cs
- RecurringJobAttribute.cs

См. AGENT_CONTEXT.md и DEPENDENCY.md.
