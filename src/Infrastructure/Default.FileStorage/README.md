# Default.FileStorage

S3-compatible object storage abstraction and DI registration.

## Scope

- Blob upload/download infrastructure — bucket policy and object naming belong in domain code.

## Integration

- **Project references:** `Default.Abstractions`, `Default.DependencyInjection`
- **Consumers:** `Default.Api`

## DI / configuration

- **DI extensions:** `S3ServiceCollectionExtensions.cs`
- **Config sections:** `S3ConfigSection.cs`

## Key files

- `FileStorageService.cs`
- `IFileStorageService.cs`
- `S3ConfigSection.cs`
- `S3ServiceCollectionExtensions.cs`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
