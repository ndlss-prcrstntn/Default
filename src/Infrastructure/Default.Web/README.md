# Default.Web

HTTP/API utilities: standardized error payloads, JWT config binding, and JSON converters used by the host.

## Scope

- Transport-layer helpers — not domain validation or business rules.

## Integration

- **Project references:** `Default.Cache`
- **Consumers:** `Default.Api`

## DI / configuration

- **DI extensions:** none in this project.
- **Config sections:** `JwtConfigSection.cs`

## Key files

- `Errors\ApiException.cs`
- `Errors\ApiResponse.cs`
- `Errors\ApiValidationErrorResponse.cs`
- `JwtConfigSection.cs`
- `TimeSpanConverter.cs`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
