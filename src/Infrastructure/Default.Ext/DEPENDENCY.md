# Default.Ext — Dependencies

## Framework

- Target framework: `net10.0`
- Nullable: `unset`
- Implicit usings: `enable`

## NuGet packages

- Castle.Core (5.2.1)
- Microsoft.Extensions.Options (10.0.3)

## Internal project references

- Default.Abstractions

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
