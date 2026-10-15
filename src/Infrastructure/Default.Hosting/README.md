# Default.Hosting

Host builder extensions, environment naming, version extraction, and ordered startup tasks.

## Scope

- Generic hosting utilities — not product-specific startup orchestration (that stays in `Default.Api`).

## Integration

- **Project references:** `Default.Abstractions`, `Default.Syntax`
- **Consumers:** `Default.Logs`, `Default.Scheduler`, `Default.Security`

## DI / configuration

- **DI extensions:** `StartupTasks\StartupTaskServiceCollectionExtensions.cs`
- **Config sections:** none

## Key files

- `AssemblyVersionExtractor.cs`
- `EnvironmentExtensions.cs`
- `EnvironmentNames.cs`
- `HostApplicationBuilderExtensions.cs`
- `StartupTasks\Filter\StartupFilter.cs`
- `StartupTasks\IStartupTask.cs`
- `StartupTasks\StartupTaskApplicationBuilderExtensions.cs`
- `StartupTasks\StartupTaskServiceCollectionExtensions.cs`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
