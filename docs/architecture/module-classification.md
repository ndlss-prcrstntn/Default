# Module Classification

## System Classification Summary

Scope scanned:

- All `.csproj` files under `src/`.
- Major solution folders: `<Host.Api>`, `Modules`, `Infrastructure`, `<Client>`.

Current system shape:

- Single primary ASP.NET host: `src/<Host.Api>`.
- Extracted modules: `src/Modules/*`.
- Infrastructure libraries: `src/Infrastructure/*`.
- Client zone: `src/<Client>`.

## Explicit Transitional Zones Inside `<Host.Api>`

List host-local mixed-ownership folders (examples: `DependencyInjection/`, `Startup/`, `DataAccess/Migrations/`, `AutoMapper/`).

## Project Classification

| Project | Type | Responsibility | Notes |
| --- | --- | --- | --- |
| `src/<Host.Api>/…` | Host Application | Composition root, transport, startup | |
| `src/Modules/…` | Domain/Feature Module | … | |
| `src/Infrastructure/…` | Infrastructure / Shared Abstractions | … | |

Types to use consistently:

- Host Application
- Domain/Feature Module
- Infrastructure Module
- Shared Kernel / Shared Types / Abstractions
- Data Access Support
- Client Application

## Boundary Warning

Folder structure is not equal to module boundaries.

Base decisions on references, runtime wiring, and ownership of behavior — not folder names alone.
