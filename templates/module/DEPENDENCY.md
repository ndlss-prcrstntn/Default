# <Project.ModuleName> — Dependencies

## Framework

- Target framework: `<Tfms>`
- Nullable / implicit usings: …

## NuGet packages

| Package | Purpose |
| --- | --- |
| … | … |

## Internal project references

| Project | Why |
| --- | --- |
| … | … |

## Architectural Dependency Rules

**Allowed:**

- This module → approved Infrastructure / Shared abstractions
- Host → this module

**Forbidden without architecture decision:**

- This module → `<Host.Api>`
- Circular module references
- Reaching into another module’s internals

## Contract Stability

Stable contracts AI must not change unless the task explicitly requires it:

- …

## Runtime dependencies

- Config keys: …
- External systems: …

## External System Boundary

- Allowed providers: …
- Forbidden alternatives without ADR: …

## Coupling / Change Impact

Changing … affects …

## Safe Change Checklist

- [ ] Public interfaces preserved or versioned intentionally
- [ ] DI registration still valid
- [ ] Tests updated/run
- [ ] CHANGELOG entry if substantive
- [ ] `src/VERSION` bumped for shipped PR
