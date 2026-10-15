# DEPENDENCY

## Framework
- TargetFramework: 
net10.0
- Nullable: 
enable
- ImplicitUsings: 
enable


## NuGet Packages
- Castle.Core (5.2.1)
- Npgsql (10.0.0)
- StackExchange.Redis (2.11.8)
- OpenTelemetry.Exporter.Prometheus.AspNetCore (1.9.0-beta.2)
- OpenTelemetry.Extensions.Hosting (1.15.0)
- OpenTelemetry.Instrumentation.AspNetCore (1.15.0)
- OpenTelemetry.Instrumentation.Http (1.15.0)
- OpenTelemetry.Instrumentation.Process (0.5.0-beta.6)
- OpenTelemetry.Instrumentation.Runtime (1.15.0)
- Sentry.AspNetCore (6.1.0)
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
- FrameworkReference: Microsoft.AspNetCore.App

## Project References
- Default.DependencyInjection
- Default.Hosting

## Consumers
- Default.Api
- Default.Scheduler

## Dependency Rules
- Allowed: directions declared in ProjectReference.
- Forbidden: circular or reverse host coupling without decision.

## Contract Stability
- Public types and extension methods are high-impact.
