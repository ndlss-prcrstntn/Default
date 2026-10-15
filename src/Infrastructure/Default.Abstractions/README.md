# Default.Abstractions

Shared contracts and cross-cutting types for the Default skeleton — an **Infrastructure** library at the bottom of the dependency graph.

## Scope

- Public interfaces, config section markers, and shared exceptions.
- Must not reference the host or domain modules.

## Integration

- **Project references:** none (leaf library).
- **Consumers:** `Default.Cache`, `Default.DependencyInjection`, `Default.Ext`, `Default.FileStorage`, `Default.Hosting`, `Default.Mail`, `Default.SignalR`.

## DI / configuration

- **DI extensions:** none in this project.
- **Config sections:** `IConfigSection.cs` pattern.

## Key files

- `Exceptions\` — shared client/service exceptions
- `Realtime\IEntityRealtimePublisher.cs`
- `Security\ISecurityContext.cs`
- `IConfigSection.cs`
- `ITraceEntity.cs`
- `IVersionExtractor.cs`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
