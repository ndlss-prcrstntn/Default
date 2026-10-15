# Default.Cache

Redis (KeyDB) cache storage abstraction and connection factory.

## Scope

- Distributed cache infrastructure — cache keys and invalidation rules belong in modules.

## Integration

- **Project references:** `Default.Abstractions`, `Default.DependencyInjection`
- **Consumers:** `Default.Web`

## DI / configuration

- **DI extensions:** `KeyDbServiceCollectionExtensions.cs`
- **Config sections:** `KeyDbConfigSection.cs`

## Key files

- `Abstractions\ICacheStorage.cs`
- `Abstractions\ICacheStorageKey.cs`
- `CacheStorage.cs`
- `CacheStorageKey.cs`
- `KeyDbConfigSection.cs`
- `KeyDbConnectionFactory.cs`
- `KeyDbServiceCollectionExtensions.cs`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
