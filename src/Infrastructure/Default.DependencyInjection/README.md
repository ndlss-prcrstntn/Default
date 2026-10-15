# Default.DependencyInjection

Shared DI utilities: assembly scanning, configuration binding helpers, and interceptor registration.

## Scope

- Composition helpers consumed by other infrastructure libraries — not module registration (host owns that).

## Integration

- **Project references:** `Default.Abstractions`, `Default.Syntax`
- **Consumers:** `Default.Cache`, `Default.DataAccess`, `Default.FileStorage`, `Default.Logs`, `Default.Mail`

## DI / configuration

- **DI extensions:** `InterceptorServiceCollectionExtensions.cs`
- **Config sections:** none

## Key files

- `AssemblyExtensions.cs`
- `ConfigurationExtensions.cs`
- `InterceptorServiceCollectionExtensions.cs`
- `ServicesCollectionDescriptorExtensions.cs`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
