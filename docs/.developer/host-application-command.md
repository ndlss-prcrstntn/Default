Create or update: `/docs/architecture/host-application.md`

Context:

- Single host application: `<Host.Api>`.
- Hybrid modular monolith.

Must define:

1. Host definition vs module role.
2. Allowed host responsibilities (transport, DTOs, mapping, DI/startup, transitional orchestration).
3. Explicit non-role: host is NOT a domain module.
4. Evolution policy: legacy-tolerant but extraction-first for new work.
5. Shared constraints (no domain invariants, no boundary bypass, persistence only via approved contracts).
6. Pointer to transitional-zones doc.

Use MUST / MUST NOT language where enforcing rules.
