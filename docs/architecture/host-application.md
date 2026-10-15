# Host Application Governance

## 1) Host Definition

### System Type

Hybrid modular monolith with a single ASP.NET host application.

### Host Role vs Module Role

The host (`<Host.Api>`) is the runtime shell that composes modules and infrastructure into one deployable process.

Modules own business capabilities, domain behavior, policies, invariants, and use-case logic.

The host is responsible for transport, composition, orchestration boundaries, and cross-cutting runtime concerns. The host is not the owner of business rules.

## 2) `<Host.Api>` (Primary Host)

### Architectural Role

- Composition Root
- Transport Layer
- Orchestration Shell

### Allowed Responsibilities

- Controllers/endpoints
- Transport DTOs and request/response contracts
- Mapping between transport contracts and module contracts
- Startup wiring and DI configuration
- Cross-cutting host runtime setup (middleware, auth wiring, observability)
- Transitional orchestration services needed while extraction is in progress

### Explicit Non-Role

`<Host.Api>` is NOT a domain module and must not become a long-term home for domain behavior.

### Evolution Policy

- Existing legacy orchestration may remain temporarily.
- New work must prefer module placement over host placement.
- Host-level logic must trend toward reduction over time.

## 3) Shared Constraints

The host MUST NOT:

- Own domain invariants
- Implement aggregate business logic
- Bypass module boundaries
- Access persistence directly outside approved module/infrastructure contracts

## 4) Transitional Zone Guidance

See `transitional-zones.md` for the concrete inventory and agent rules.
