# Default.SignalR

ASP.NET Core SignalR helpers for realtime updates: `EntityHub` at `/hub/entities` and `IEntityRealtimePublisher` for typed entity-change notifications.

## Scope

- Hub mapping and DI registration extensions.
- Does not own domain rules — feature modules publish changes through the publisher abstraction.

## Integration

- **Consumers:** host (`Default.Api`) maps hubs at startup.

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
