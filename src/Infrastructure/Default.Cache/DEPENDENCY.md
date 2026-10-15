# Default.Cache — Dependencies

## Framework

- Target framework: `net10.0`
- Nullable: `unset`
- Implicit usings: `enable`

## NuGet packages

- Microsoft.Extensions.Configuration.Abstractions (10.0.3)
- Microsoft.Extensions.DependencyInjection.Abstractions (10.0.3)
- Newtonsoft.Json (13.0.4)
- StackExchange.Redis (2.11.8)

## Internal project references

- Default.Abstractions
- Default.DependencyInjection

## Consumers

- Default.Web

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
