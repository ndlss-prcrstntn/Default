# Host and Module Boundaries

## Purpose

Enforceable architectural boundaries between `<Host.Api>` (host) and business modules to prevent architectural drift.

## 1) Host Boundaries (`<Host.Api>`)

### Host responsibilities

`<Host.Api>` MUST contain only host-level concerns:

- Transport (HTTP endpoints, routing, auth integration at the edge)
- Orchestration of module use cases
- Request/response DTO definitions
- Controllers and endpoint handlers
- Mapping between transport DTOs and module contracts
- Startup/configuration wiring

### Transitional orchestration-heavy areas

- Existing orchestration-heavy or mixed logic MUST be treated as legacy debt.
- Transitional logic MAY remain temporarily only when immediate extraction is not feasible.
- Transitional logic MUST NOT be used as precedent for new design.
- New functionality MUST NOT increase legacy surface area in the host.

### Gradual extraction strategy

1. Identify business decision points currently inside host code.
2. Introduce or extend a module contract that owns those decisions.
3. Move business logic into the module behind that contract.
4. Reduce host code to orchestration, DTO translation, and flow coordination.
5. Keep behavior stable with regression tests while extraction proceeds.

## 2) Module Boundaries

### Ownership of business logic

- Business invariants, policies, and decision rules MUST live in modules.
- Validation that encodes business meaning MUST live in modules.
- Module state transitions MUST be enforced by module-owned logic.

### Dependency direction

- `<Host.Api>` MUST depend on modules (and infrastructure adapters).
- Modules MUST NOT depend on `<Host.Api>`.
- Modules MUST remain host-agnostic.

### Contract integrity

- Host code MUST invoke modules through explicit module contracts.
- Host code MUST NOT bypass module contracts to manipulate internals or persistence.
- Cross-module interactions MUST use approved module contracts.

## 3) Anti-Pattern Prevention

AI and contributors MUST NOT:

- Move new business rules into host services, controllers, handlers, or host-level mappers.
- Create new legacy layers inside the host without an explicit architectural decision.
- Couple modules back to the host through direct references or host DTO dependencies.

### Required behavior when boundary pressure appears

- Pause and define/extend module contracts first.
- If temporary host logic is forced: mark as transitional debt and include extraction path.
- Reviewers MUST reject PRs that add net-new business rule ownership to the host without approval.

## Compliance Checklist (PR level)

- [ ] No new business invariants/policies were added to `<Host.Api>`.
- [ ] Host changes are limited to transport/orchestration/DTO/mapping/startup wiring.
- [ ] Module contracts were used and not bypassed.
- [ ] Dependency direction remains host → modules.
- [ ] Transitional host logic was reduced, or extraction follow-up was documented.
