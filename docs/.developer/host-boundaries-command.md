Create or update: `/docs/architecture/host-boundaries.md`

Context:

- Single host: `<Host.Api>`.
- Risk: host absorbing module-owned business logic.

Must define:

1. Host boundaries — allowed concerns; transitional debt rules; gradual extraction strategy.
2. Module boundaries — ownership of invariants; dependency direction; contract integrity.
3. Anti-pattern prevention for AI and contributors.
4. PR-level compliance checklist.

Use strict MUST / MUST NOT language.
