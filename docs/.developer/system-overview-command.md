Create or update: `/docs/architecture/system-overview.md`

Scan the repository and write **current facts** (not aspirational architecture).

Must cover:

1. Architectural style (hybrid modular monolith unless evidence says otherwise).
2. Host role (`<Host.Api>`): TFM, composition root responsibilities, notable transitional orchestration.
3. Module layer: list `src/Modules/*` with one-line roles.
4. Infrastructure layer: list `src/Infrastructure/*` with one-line roles.
5. Optional access/security model only if present in code/docs.
6. Transitional zones currently in the host.
7. Textual flow: `<Client> -> <Host.Api> -> Modules/* -> Infrastructure/*`.

Rules:

- Use project names from `.csproj` files.
- Do not invent missing folders (`Shared`, `DataAccess`, etc.) unless they exist.
- Keep product-specific domain detail minimal; focus on structure and ownership.
