# AI Constitution — Hybrid Modular Monolith Governance

Generate or update `.specify/memory/constitution.md` for `<Project>`.

## 1. Authority and Hierarchy of Truth

### Global authoritative documents (when present)

- `/docs/architecture/system-overview.md`
- `/docs/architecture/module-classification.md`
- `/docs/architecture/host-application.md`
- `/docs/architecture/host-boundaries.md`
- `/docs/architecture/transitional-zones.md`

### Module-local authority

For any `<MODULE_PATH>`, read and follow:

1. `AGENT_CONTEXT.md`
2. `DEPENDENCY.md`
3. `README.md`

If documentation conflicts with code, follow Source Of Truth in AGENT_CONTEXT and record Known Debt.

AI MUST NOT invent architecture, module boundaries, or ownership when facts are documented.

## 2. System Architecture Model

Hybrid Modular Monolith with:

- One ASP.NET host: `<Host.Api>`
- Client: `<Client>`
- Modules under `src/Modules`
- Infrastructure under `src/Infrastructure`
- Transitional legacy logic concentrated mainly in the host

Key rules:

- `.csproj` boundaries are primary architectural units.
- Folder structure inside the host does not equal module boundaries.
- Host orchestrates modules; modules own business logic.

## 3. Host Application Governance

Host MAY: controllers/endpoints, transport DTOs, mapping, DI/config, transitional orchestration.

Host MUST NOT: own core domain invariants, become a hidden business layer, bypass module boundaries.

## 4. Transitional Zone Policy

AI MUST prefer extraction to modules when adding business behavior.

AI MUST NOT add new core business logic to transitional host areas by default.

## 5. Contract Stability

- No domain entities as API contracts.
- Prefer strongly-typed action results for client generation.
- Analyze `<Client>` impact for API changes.

## 6. Delivery Protocol

Before: classify target → read docs → plan smallest boundary-respecting change.

After: preserve contracts → verify build/tests → document Known Debt → CHANGELOG + VERSION when substantive.

Use MUST / MUST NOT language. Keep product names as placeholders until filled.
