# Default.FileStorage — Dependencies

## Framework

- Target framework: `net10.0`
- Nullable: `enable`
- Implicit usings: `enable`

## NuGet packages

- AWSSDK.Extensions.NETCore.Setup (4.0.3.23)
- AWSSDK.S3 (4.0.18.7)
- Microsoft.Extensions.Options (10.0.3)

## Internal project references

- Default.Abstractions
- Default.DependencyInjection

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
