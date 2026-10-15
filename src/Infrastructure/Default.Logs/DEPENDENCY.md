# Default.Logs — Dependencies

## Framework

- Target framework: `net10.0`
- Nullable: `enable`
- Implicit usings: `enable`

## NuGet packages

- Castle.Core (5.2.1)
- Npgsql (10.0.0)
- StackExchange.Redis (2.11.8)
- Sentry.Serilog (6.1.0)
- Serilog (4.3.1)
- Serilog.AspNetCore (10.0.0)
- Serilog.Enrichers.Environment (3.0.1)
- Serilog.Enrichers.Thread (4.0.0)
- Serilog.Expressions (5.0.0)
- Serilog.Extensions.Hosting (10.0.0)
- Serilog.Settings.Configuration (10.0.0)
- Serilog.Sinks.Console (6.1.1)
- Serilog.Sinks.Seq (9.0.0)
- SerilogTracing.Expressions (2.3.1)
- OpenTelemetry.Exporter.Prometheus.AspNetCore (1.9.0-beta.2)
- OpenTelemetry.Extensions.Hosting (1.15.0)
- OpenTelemetry.Instrumentation.AspNetCore (1.15.0)
- OpenTelemetry.Instrumentation.Http (1.15.0)
- OpenTelemetry.Instrumentation.Process (0.5.0-beta.6)
- OpenTelemetry.Instrumentation.Runtime (1.15.0)
- Sentry.AspNetCore (6.1.0)

## Internal project references

- Default.DependencyInjection
- Default.Hosting

## Consumers

- Default.Api

## Architectural Dependency Rules

**Allowed:**

- This library → approved Infrastructure peers listed above
- Host / modules → this library

**Forbidden without an architecture decision:**

- This library → ``Default.Api``
- Circular infrastructure references
- Reaching into module internals from infrastructure

## Contract Stability

- Public types, DI extension methods, and config section shapes are high-impact.
- Prefer additive changes; document breaking changes in ``CHANGELOG.md`` and bump ``src/VERSION``.

## Safe Change Checklist

- [ ] Public interfaces preserved or intentionally versioned
- [ ] DI registration still valid in the host
- [ ] ``CHANGELOG.md`` entry if substantive
- [ ] ``src/VERSION`` bumped for a shipped PR
