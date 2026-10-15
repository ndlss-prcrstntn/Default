# Default.Logs

Serilog logging, metrics, and observability health checks for the host and scheduler.

## Scope

- Cross-cutting logging configuration and sensitive-data destructuring policies.

## Integration

- **Project references:** `Default.DependencyInjection`, `Default.Hosting`
- **Consumers:** `Default.Api`, `Default.Scheduler`

## DI / configuration

- **DI extensions:** `LoggerServiceCollectionExtension.cs`, `MetricsServiceCollectionExtension.cs`, `ObservabilityHealthChecksExtensions.cs`
- **Config sections:** `LogConfigSection`; health checks read `dataAccess:connection`, `keyDB:connection`, `s3:endpoint`

## Key files

- `Config\LogConfigSecition.cs`
- `ISerilogSinkConfigurator.cs`
- `LoggerServiceCollectionExtension.cs`
- `LoggingApplicationBuilderExtensions.cs`
- `LoggingInterceptor.cs`
- `MetricsServiceCollectionExtension.cs`
- `ObservabilityApplicationBuilderExtensions.cs`
- `ObservabilityHealthChecksExtensions.cs`
- `SensitiveAttribute.cs`
- `TracedObjectDestructuringPolicy.cs`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
