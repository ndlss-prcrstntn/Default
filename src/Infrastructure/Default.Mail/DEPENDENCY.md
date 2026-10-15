# Default.Mail — Dependencies

## Framework

- Target framework: `net10.0`
- Nullable: `enable`
- Implicit usings: `enable`

## NuGet packages

- MailKit (4.15.0)
- Microsoft.Extensions.Configuration.Abstractions (10.0.3)
- Microsoft.Extensions.Logging.Abstractions (10.0.3)
- Microsoft.Extensions.Options (10.0.3)
- MimeKit (4.15.1)

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
