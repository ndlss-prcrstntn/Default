# Default Development Guidelines

Auto-generated / manually maintained agent brief. Last updated: 2026-10-15

## Active Technologies

- C# / .NET (`net10.0`) + ASP.NET Core, EF Core
- Modular DI: host `Default.Api`, modules under `src/Modules`, infrastructure under `src/Infrastructure/Default.*`
- Frontend: `Default.Client` (Ionic Angular skeleton)
- OpenAPI client generation: NSwag (or equivalent) — prefer strongly-typed controller return types

## Project Structure

```text
src/
  Default.Api/         # composition root / transport host
  Default.Client/      # Ionic Angular skeleton
  Modules/             # domain / feature modules (empty starter)
  Infrastructure/      # Default.* technical libraries
  Default.slnx
tests/
docs/
  architecture/        # system truth for agents
  .developer/          # prompts to regenerate docs
.cursor/rules/         # always-on agent rules
.specify/              # constitution + feature templates
scripts/               # repo tooling (e.g. add-migration)
```

## Commands

```bash
dotnet build
dotnet test
# frontend (adjust):
npm test
npm run lint
```

## Code Style

- Follow existing conventions in the repo.
- Prefer why-comments on public APIs, invariants, auth gates.
- Do not narrate obvious code; do not mass-comment unprompted.

<!-- MANUAL ADDITIONS START -->

## App version

- Source of truth: `src/VERSION` (semver `MAJOR.MINOR.PATCH`).
- Assembly / `GET /version` read it via `src/Directory.Build.props`.
- **Bump `src/VERSION` on every shipped PR** (MINOR for features; PATCH for tiny fixes).
- See `.cursor/rules/app-versioning.mdc`.

## Module CHANGELOG.md

- Each module / infrastructure library / host / client keeps `CHANGELOG.md` at its root.
- After substantive changes: dated entry (1–5 bullets on behavior/contract). No file lists.
- `AGENT_CONTEXT.md` / `README.md` = current truth; `CHANGELOG.md` = history.
- See `.cursor/rules/module-changelog-and-comments.mdc`.

## EF migrations

- Create migrations **only** via `.\scripts\add-migration.ps1 <Name>` (or equivalent `dotnet ef` on the host).
- Never hand-write migration `.cs` / Designer / snapshot.
- See `.cursor/rules/ef-migrations.mdc`.

## Architecture

- Hybrid modular monolith: host orchestrates; modules own business rules.
- Read `docs/architecture/` and module `AGENT_CONTEXT.md` before editing.
- See `.specify/memory/constitution.md`.

<!-- MANUAL ADDITIONS END -->
