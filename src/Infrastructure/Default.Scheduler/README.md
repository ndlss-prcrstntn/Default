# Default.Scheduler

Background job scheduling (Hangfire) abstractions, recurring job registration, and startup wiring.

## Scope

- Scheduler infrastructure — job implementations live in modules or the host orchestration layer.

## Integration

- **Project references:** `Default.DataAccess`, `Default.Hosting`
- **Consumers:** none (registered by host)

## DI / configuration

- **DI extensions:** `DependencyInjection\HangfireServiceCollectionExtensions.cs`, `RecurringJobsStartupTask.cs`
- **Config sections:** none in this project

## Key files

- `Abstractions\BaseRecurringJob.cs`
- `Abstractions\IJobService.cs`
- `Abstractions\IRecurringJob.cs`
- `DependencyInjection\HangfireServiceCollectionExtensions.cs`
- `DependencyInjection\RecurringJobsStartupTask.cs`
- `JobDescriptor.cs`
- `JobService.cs`
- `Model\JobInfo.cs`
- `Model\JobState.cs`
- `RecurringJobAttribute.cs`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
