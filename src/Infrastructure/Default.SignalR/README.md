# Default.SignalR

ASP.NET Core SignalR helpers for realtime updates: a sample hub and a publisher contract so modules can push typed entity-change notifications to connected clients.

## Scope

- Hub mapping and DI registration extensions.
- Does not own domain rules — feature modules publish changes through the publisher abstraction.

## Integration

- **Consumers:** host (`Default.Api`) maps hubs at startup.
- Rename sample hub/route names when adopting this skeleton for a product.

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
