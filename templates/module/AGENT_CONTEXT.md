# <Project.ModuleName> — Agent Context

## Module Purpose

One paragraph: what this module owns.

## Source Of Truth Priority

1. Code in this module.
2. Host wiring (DI extension / composition root call site).
3. This file / DEPENDENCY.md / README.md.

## Current Runtime Wiring

- DI: …
- Config section(s): …
- Key lifetimes: …
- HTTP (if any): …

## External Call Sites (High Impact)

- …

## Behavior Invariants

- …
- …

## Known Debt / Sharp Edges

- …

## Editing Rules For Agents

- Forbidden without explicit request: …
- Requires synchronized changes: …
- No dependency on `<Host.Api>`.

## Minimal Verification After Changes

```bash
dotnet test tests/Modules/<Project.ModuleName>.Tests/<Project.ModuleName>.Tests.csproj
```
