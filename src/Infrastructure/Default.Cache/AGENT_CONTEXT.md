# Default.Cache — Agent Context

## Module Purpose

Redis/KeyDB cache storage abstraction and connection factory.

## Source Of Truth Priority

1. Code and `Default.Cache.csproj` in this project.
2. Host wiring in ``Default.Api`` when this library is registered there.
3. This file, then ``DEPENDENCY.md``, then ``README.md``.

## Current Runtime Wiring

- Target framework: `net10.0`
- Nullable: `unset`
- Implicit usings: `enable`

## External Call Sites (High Impact)

- Consumers: Default.Web

## Behavior Invariants

- Stay an infrastructure capability — do not absorb domain rules.
- Preserve public contracts unless the task explicitly versions a breaking change.
- Keep dependency direction: modules/host → infrastructure; never reverse into the host.

## Known Debt / Sharp Edges

- Rename sample hub/route/type names to match your product when you adopt this skeleton.

## Editing Rules For Agents

- Do not create circular project references.
- Do not move unrelated layer logic into this library.
- Prefer synchronized updates across consumers when changing public surfaces.

## Minimal Verification After Changes

```bash
dotnet build src/Default.slnx
```
