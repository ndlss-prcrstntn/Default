# Architecture doc generation order

Run / apply these prompts in order when bootstrapping or refreshing architecture docs for a new solution.

1. `module-description-command.md` — per module (README / AGENT_CONTEXT / DEPENDENCY)
2. `system-overview-command.md`
3. `module-classification-command.md`
4. `host-application-command.md`
5. `host-boundaries-command.md`
6. `transitional-zones-command.md`
7. `constitution-command.md` — align `.specify/memory/constitution.md` with facts
8. `module-changelog-command.md` — ongoing maintenance (not bootstrap-only)

Optional (if stack matches):

- Frontend client / shared Angular module prompts — add when needed; keep product-specific.
