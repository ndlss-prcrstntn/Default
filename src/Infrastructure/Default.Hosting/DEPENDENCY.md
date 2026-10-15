# Default.Hosting — Dependencies

## Framework

- Target framework: `net10.0`
- Nullable: `enable`
- Implicit usings: `enable`

## NuGet packages

- Microsoft.AspNetCore.Hosting.Abstractions (2.3.9)
- Microsoft.AspNetCore.Http.Abstractions (2.3.9)
- Microsoft.Extensions.Configuration (10.0.3)
- Microsoft.Extensions.Configuration.Json (10.0.3)
- Microsoft.Extensions.Configuration.EnvironmentVariables (10.0.3)
- Microsoft.Extensions.Configuration.FileExtensions (10.0.3)
- Microsoft.Extensions.Configuration.UserSecrets (10.0.3)
- Microsoft.Extensions.Hosting.Abstractions (10.0.3)
- Scrutor (7.0.0)
- Serilog (4.3.1)

## Internal project references

- Default.Abstractions
- Default.Syntax

## Consumers

- Default.Logs, Default.Scheduler

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
