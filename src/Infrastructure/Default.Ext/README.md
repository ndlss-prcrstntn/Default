# Default.Ext

Miscellaneous host-side utilities: licensing hooks, crypto helpers, and UID locking.

## Scope

- Optional product licensing and low-level helpers — keep domain logic out of this library.

## Integration

- **Project references:** `Default.Abstractions`
- **Consumers:** `Default.Api`

## DI / configuration

- **DI extensions:** none
- **Config sections:** `LicenseConfigSection.cs`

## Key files

- `CryptoGenerator.cs`
- `License\DateEncryptor.cs`
- `License\LicenseConfigSection.cs`
- `License\LicenseException.cs`
- `License\LicenseService.cs`
- `UidLockManager.cs`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
