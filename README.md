# Default project skeleton

Чистый переиспользуемый скелет hybrid modular monolith: правила, инфраструктура `Default.*`, host `Default.Api`, клиент `Default.Client`.

Содержит:

- правила для AI/агентов (`.cursor/rules`)
- конституцию и governance (`.specify`)
- шаблоны архитектурных документов (`docs/architecture`)
- промпты для генерации/обновления документов (`docs/.developer`)
- шаблоны модульной документации (`templates/module`)
- версионирование (`src/VERSION` + `Directory.Build.props`)
- инфраструктуру `src/Infrastructure/Default.*` (DataAccess, Logs, Mail, S3, Cache, …)
- host `src/Default.Api` (composition root без доменных модулей)
- клиент-скелет `src/Default.Client` (Ionic Angular: Home/Profile)
- `scripts/add-migration.ps1`
- `src/Default.slnx`

## Как использовать как ядро нового продукта

1. Клонировать этот репозиторий (или скопировать содержимое в корень нового репо).
2. Заменить плейсхолдеры / переименовать `Default` → префикс продукта (см. ниже).
3. Заполнить `docs/architecture/*` фактами нового решения (или прогнать команды из `docs/.developer`).
4. Подключить `.cursor/rules` — они уже `alwaysApply` / с globs.
5. При появлении первого модуля — скопировать `templates/module/*` в его корень и заполнить.

Чеклист: `ADOPTION.md`.

## Быстрый старт

```bash
# Backend
dotnet restore src/Default.slnx
dotnet run --project src/Default.Api

# Frontend
cd src/Default.Client && npm install && npm start
```

## Переименование `Default` → ваш префикс

Проекты и namespace уже названы `Default.*`. Для продукта:

1. Rename folders/projects `Default.X` → `YourBrand.X`
2. Replace in code/csproj: `Default` → `YourBrand` (осторожно с словом Default в C# API)
3. Обновить `scripts/add-migration.ps1`, `AGENTS.md`, constitution

Документы с плейсхолдерами `<Project>` / `<Host.Api>` / `<Client>` можно синхронизировать с реальными именами.

## Что намеренно НЕ включено

- Доменные модули (Events/Artists/…) — только пустой `src/Modules/`
- Продуктовые правила контента и ACL каталога
- CI/CD / Docker / VPS runbooks
- Полный OpenAPI/NSwag-клиент (добавляется по мере API)

## Порядок истины для агентов

1. `.specify/memory/constitution.md`
2. `docs/architecture/*`
3. `<module>/AGENT_CONTEXT.md` → `DEPENDENCY.md` → `README.md`
4. Код (если docs расходятся — фиксировать Known Debt в AGENT_CONTEXT)

См. также `AGENTS.md` и `structure/LAYOUT.md`.
