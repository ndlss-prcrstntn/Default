# Default.DataAccess — Dependencies

## Framework

- Target framework: `net10.0`
- Nullable: `unset`
- Implicit usings: `enable`

## NuGet packages

- FlexLabs.EntityFrameworkCore.Upsert (10.0.0)
- Microsoft.EntityFrameworkCore (10.0.3)
- Microsoft.EntityFrameworkCore.Analyzers (10.0.3)
- Microsoft.EntityFrameworkCore.Design (10.0.3)
- Microsoft.EntityFrameworkCore.Proxies (10.0.3)
- Microsoft.EntityFrameworkCore.Relational (10.0.3)
- Microsoft.Extensions.Hosting.Abstractions (10.0.3)
- Npgsql.EntityFrameworkCore.PostgreSQL (10.0.0)

## Internal project references

- Default.DependencyInjection

## Consumers

- Default.Api, Default.Scheduler

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
