Create or update: `/docs/architecture/transitional-zones.md`

Goal: inventory real transitional/legacy zones in `<Host.Api>` from the repository snapshot.

Must:

1. Define what a Transitional Zone is.
2. List explicit zones with current state, why transitional, migration intent.
3. State AI agent rules (prefer extraction; no new core business logic by default; stabilization allowed).
4. Describe evolution strategy: strangler, gradual modularization, behavior-preserving refactors.

Do not invent folders that do not exist. If a classic zone is missing, say so and note where similar concerns live instead.
