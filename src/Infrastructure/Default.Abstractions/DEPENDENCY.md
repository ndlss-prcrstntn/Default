# Default.Abstractions — Dependencies

## Framework

- Target framework: `net10.0`
- Nullable: `enable`
- Implicit usings: `enable`

## NuGet packages

- (framework / transitive only)

## Internal project references

- none (leaf library)

## Consumers

- Default.Cache, Default.DependencyInjection, Default.Ext, Default.FileStorage, Default.Hosting, Default.Mail, Default.SignalR

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
