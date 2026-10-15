# Architecture docs

These files are the **system truth** for humans and AI agents.

Fill them with facts from the real solution (projects, refs, wiring). Prefer regenerating via prompts in `docs/.developer/` rather than inventing target-state fiction.

| Doc | Purpose |
| --- | --- |
| `system-overview.md` | Style, host role, module/infra lists, high-level flow |
| `module-classification.md` | Per-`.csproj` type, responsibility, maturity |
| `host-application.md` | What the host is / is not |
| `host-boundaries.md` | Enforceable host ↔ module rules |
| `transitional-zones.md` | Legacy host areas + extraction rules |

Suggested generation order: see `docs/.developer/command-rules.md`.
