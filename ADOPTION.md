# Adoption checklist

Use when starting a new product from this core skeleton.

## 1. Bootstrap

- [ ] Clone this repository (or copy its contents) into the new product repo root.
- [ ] Keep `origin` pointed at the product repo (not at this core), unless you intentionally track upstream.

## 2. Rename `Default` → product prefix (optional)

- [ ] Rename `Default.*` projects/folders/namespaces
- [ ] Update `scripts/add-migration.ps1`, solution name, client `package.json`
- [ ] Sync doc placeholders `<Project>` / `<Host.Api>` / `<Client>`

## 3. Wire runtime

- [ ] `src/VERSION` + `Directory.Build.props` already wired for `Default.Api`
- [ ] Copy `appsettings.Development.local.json.example` → local secrets
- [ ] Postgres / MinIO / Mailpit as needed for infra you enable
- [ ] `dotnet build src/Default.slnx` and `npm install` in `Default.Client`

## 4. Generate architecture facts

Follow `docs/.developer/command-rules.md`:

- [ ] Per-module triad via `module-description-command.md`
- [ ] `system-overview` → `module-classification` → host docs → transitional zones
- [ ] Align `.specify/memory/constitution.md`

## 5. Cursor / agents

- [ ] Confirm `.cursor/rules/*.mdc` are picked up in the new workspace.
- [ ] Fill `AGENTS.md` Active Technologies / Commands for the real stack.
- [ ] Add product-specific rules as **separate** `.mdc` files (do not overload the generic ones).

## 6. First feature

- [ ] Copy `templates/module/*` into the new module folder and fill.
- [ ] Optionally use `.specify/templates` for spec → plan → tasks.

## Out of scope for this skeleton

Add separately per product: CI/CD, Docker, deploy runbooks, and your chosen auth / identity model.
