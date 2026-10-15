# Default Constitution

## Core Principles

### I. Authority and Hierarchy of Truth
AI MUST treat architecture documents in `docs/architecture/` as the primary source of
system truth. For any target module, AI MUST read and apply `AGENT_CONTEXT.md`,
`DEPENDENCY.md`, and `README.md` in that precedence order before editing code.
When documentation conflicts with implementation, AI MUST follow the source-of-truth
order declared in `AGENT_CONTEXT.md` and record the mismatch as Known Debt.
Rationale: correctness depends on documented ownership and boundaries, not inferred
structure.

### II. Hybrid Modular Monolith Boundaries
The system is a hybrid modular monolith with `Default.Api` as host, `Default.Client` as
frontend, module projects under `src/Modules`, and shared infrastructure under
`src/Infrastructure`. AI MUST treat `.csproj` boundaries as architectural units and
MUST NOT infer domain ownership from folders inside the host.
Rationale: physical project boundaries are enforceable; host folder layout is not.

### III. Host Application as Composition Root
`Default.Api` MUST be treated as host/composition root and MUST NOT become a domain
module. Host code MAY contain transport endpoints, transport DTOs, mapping,
configuration, and orchestration. Host code MUST NOT own domain invariants, implement
hidden business logic layers, or bypass module boundaries for persistence and business
operations.
Rationale: preserving host thinness prevents architectural erosion and hidden coupling.

### IV. Transitional Zone Extraction-First
Legacy orchestration-heavy areas in the host are transitional zones. AI MUST
stabilize these zones with behavior-preserving refactors and MUST prefer extraction to
modules when introducing business behavior. AI MUST NOT add new core business logic to
transitional host areas unless explicitly instructed.
Rationale: extraction-first evolution reduces long-term migration risk.

### V. Contract Stability and Change Safety
DTOs are transport contracts; domain entities MUST NOT be exposed through API
contracts. Backend API changes MUST include impact analysis for `Default.Client` and
other consumers. AI MUST NOT introduce silent breaking endpoint/DTO changes,
unannounced DI contract changes, or broad cross-module refactors without explicit
scope and approval.
Rationale: contract discipline protects independent evolution across backend and
frontend.

### VI. Controller Implementation Standards
Controllers MUST avoid returning `IActionResult` where possible. Instead, they SHOULD
return strongly-typed `ActionResult<T>` or `ActionResult` to provide clear contract
definition and improve API client generation (e.g. NSwag). Exceptions are allowed only
when dynamic or polymorphic responses are strictly necessary and documented.
Rationale: explicit return types ensure accurate API documentation and generated clients.

## Architecture and Module Governance

- Module types and ownership:
  - `src/Modules/*` own feature/domain behavior in their scope.
  - `src/Infrastructure/*` own integration and technical capabilities.
- Dependency integrity rules:
  - AI MUST respect `.csproj` boundaries.
  - AI MUST prevent circular dependencies.
  - AI MUST keep dependency direction explicit and justified in design artifacts.
- Host-level orchestration rules:
  - Host orchestrates modules via contracts/interfaces.
  - Host MUST NOT replace module-owned business behavior.

## Agent Workflow and Delivery Protocol

Before implementation, AI MUST:
1. Identify change target type: host, module, infrastructure, or transitional zone.
2. Read architecture facts and module-local governance docs.
3. Evaluate coupling and external call sites, including frontend contract impact.
4. Plan the smallest boundary-respecting change.

During and after implementation, AI MUST:
1. Preserve public interfaces unless explicitly approved to break them.
2. Verify affected build/test/integration points.
3. Document Known Debt for architecture or documentation mismatches.
4. Update module `CHANGELOG.md` and bump `src/VERSION` for substantive shipped work.

## Governance

This constitution supersedes conflicting local practices for design and implementation
work. Amendments MUST include: (1) proposed text diff, (2) migration or adoption
impact, and (3) template synchronization across `.specify/templates/*` when applicable.

Versioning policy MUST use semantic versioning:
- MAJOR: backward-incompatible governance changes or principle removals/redefinitions.
- MINOR: new principle/section or materially expanded mandatory guidance.
- PATCH: clarifications, wording, typo, and non-semantic refinements.

Compliance review expectations:
- Every implementation plan MUST pass Constitution Check gates before research and
  again after design.
- Every specification affecting contracts or boundaries MUST include explicit impact
  analysis.
- Every task plan MUST include architecture-doc review and contract/regression
  validation work where applicable.

**Version**: 1.0.0 | **Ratified**: YYYY-MM-DD | **Last Amended**: YYYY-MM-DD
