# Default.Web — Dependencies

## Framework

- Target framework: `net10.0`
- Nullable: `unset`
- Implicit usings: `enable`

## NuGet packages

- Microsoft.AspNetCore.Mvc.Abstractions (2.3.9)
- Microsoft.AspNetCore.Mvc.Core (2.3.9)
- Microsoft.Extensions.Logging.Abstractions (10.0.3)

## Internal project references

- Default.Cache

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
