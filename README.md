# Default — hybrid modular monolith starter

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](./LICENSE)

Reusable **.NET** skeleton for a hybrid modular monolith: agent rules, shared `Default.*` infrastructure, composition-root host (`Default.Api`), and an Ionic Angular client shell (`Default.Client`). Clone it, rename the prefix, add domain modules under `src/Modules/`, and keep architecture docs as the source of truth.

## Why use this

- **Clear boundaries** — `.csproj` projects are architectural units; the host orchestrates, modules own business rules.
- **Agent-ready** — `.cursor/rules`, `.specify/memory/constitution.md`, and per-module `AGENT_CONTEXT.md` / `DEPENDENCY.md` triads.
- **Batteries included (infra)** — EF Core data access, logging/metrics, mail, S3 file storage, cache, JWT helpers, SignalR, Hangfire scheduler, and DI/interceptor utilities.
- **Versioned releases** — semver in `src/VERSION`, wired through `Directory.Build.props`.

## Features

| Area | What you get |
| --- | --- |
| Backend | ASP.NET Core host, modular DI, migrations via `scripts/add-migration.ps1` |
| Frontend | Ionic Angular skeleton (Home / Profile tabs, auth interceptor stub) |
| Docs | `docs/architecture/*`, regeneration prompts in `docs/.developer/` |
| Modules | Empty `src/Modules/` + `templates/module/` documentation starters |
| Governance | Constitution, feature templates under `.specify/templates/` |

## Quick start

```bash
# Backend
dotnet restore src/Default.slnx
dotnet run --project src/Default.Api

# Frontend
cd src/Default.Client && npm install && npm start
```

Copy `appsettings.Development.local.json.example` to local secrets before running against Postgres, MinIO, Mailpit, etc. See `ADOPTION.md` for the full checklist.

## Repository layout

```text
.
├── AGENTS.md                 # agent brief (commands, stack)
├── ADOPTION.md               # new-product checklist
├── structure/LAYOUT.md       # canonical folder shape
├── .cursor/rules/            # always-on Cursor rules
├── .specify/                 # constitution + spec templates
├── docs/architecture/        # system truth (fill for your product)
├── scripts/add-migration.ps1
└── src/
    ├── VERSION
    ├── Default.slnx
    ├── Default.Api/          # composition root
    ├── Default.Client/       # Ionic Angular shell
    ├── Modules/              # domain modules (empty starter)
    └── Infrastructure/Default.*
```

Dependency direction: **Client → Api → Modules → Infrastructure**.

## Rename `Default` → your product prefix

Projects and namespaces use the `Default.*` prefix today. For a new product:

1. Rename folders/projects `Default.X` → `YourBrand.X`.
2. Replace identifiers in code and `.csproj` files (`Default` → `YourBrand` — watch for clashes with C# `default`).
3. Update `scripts/add-migration.ps1`, solution name, client `package.json`, and doc placeholders (`<Project>`, `<Host.Api>`, `<Client>`).
4. Refresh `AGENTS.md` and `.specify/memory/constitution.md` for your stack.

## Truth order for humans and agents

1. `.specify/memory/constitution.md`
2. `docs/architecture/*`
3. `<module>/AGENT_CONTEXT.md` → `DEPENDENCY.md` → `README.md`
4. Code — if docs drift, record **Known Debt** in `AGENT_CONTEXT.md`

## Intentionally out of scope

Add these per product when you need them:

- **CI/CD** pipelines and quality gates
- **Docker** images and container orchestration
- **Deploy / ops runbooks** (VPS, k8s, secrets rotation)
- **Product-specific auth model** (identity provider, SSO, fine-grained RBAC design)
- **Generated OpenAPI client** — wire NSwag (or similar) once your API surface stabilizes

Domain feature modules are not shipped here — only an empty `src/Modules/` folder and `templates/module/` starters.

## License

MIT — see [LICENSE](./LICENSE).

## Contributing

Fork or copy this repo as the core of a new product. Keep module `CHANGELOG.md` files for substantive history and bump `src/VERSION` on shipped PRs (see `.cursor/rules/app-versioning.mdc`). Stars and issues that improve the skeleton for everyone are welcome.
