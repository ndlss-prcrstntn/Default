# <Project> System Overview

> Fill with repository facts. Delete placeholder bullets as you replace them.

## 1) Architectural Style

- Current style: **hybrid modular monolith**.
- Runtime topology: **single host** (`<Host.Api>`) with internal modular/infrastructure project boundaries.
- Frontend (`<Client>`) consumes host HTTP APIs.

## 2) Host Role Overview (`<Host.Api>`)

- Primary ASP.NET Core HTTP host.
- Target framework: `<Tfms>`.
- Composition root wires: controllers/endpoints, auth, OpenAPI, startup tasks, DB migration, module/infrastructure DI extensions.
- Note any transitional orchestration still living in the host.

## 3) Module Layer (`src/Modules/*`)

List extracted modules (name, TFM, one-line role):

- `<Project>.FeatureA` — …
- `<Project>.FeatureB` — …

Role of module layer:

- Encapsulates domain/application capabilities consumed by the host.
- Integrated through DI extension methods and project references.

## 4) Infrastructure Layer (`src/Infrastructure/*`)

List infrastructure projects:

- `<Project>.DataAccess` — …
- `<Project>.Abstractions` — …

## 5) Access / security model (optional section)

Document roles, gates, and ownership only if applicable to this product.

## 6) Transitional Zones (Current State)

List host folders/files that mix transport + orchestration + partial business logic.

## 7) Textual Architecture Flow

```text
<Client> -> <Host.Api> -> Modules/* -> Infrastructure/*
```
