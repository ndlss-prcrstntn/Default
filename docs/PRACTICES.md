# Cross-cutting practices

Product-agnostic habits that traveled with this skeleton. Adapt names/paths per solution.

## Secrets and config

- Commit safe defaults and `CHANGE_ME` placeholders only.
- Local secrets: `*.local.json`, `.env`, user-secrets — never commit.
- Prefer `dotnet user-secrets` / env vars over checked-in credentials.
- Rotate anything that ever landed in git history.

## Observability (suggested)

- `GET /health/live`, `GET /health/ready`, `GET /version` as ops endpoints (not product API).
- Structured logging without secrets.
- Metrics scrape endpoint separate from client contracts.

## Testing

- Unit/module tests next to capability under `tests/Modules/…`.
- Integration tests against the host for cross-module and auth flows.
- Prefer behavior-preserving tests before extracting transitional host logic.

## PR hygiene

1. Smallest boundary-respecting change.
2. Bump `src/VERSION`.
3. Update affected module `CHANGELOG.md` entries.
4. Update AGENT_CONTEXT if invariants/wiring changed.
5. No silent breaking DTO/endpoint/DI changes.

## Frontend contract

- Prefer strongly-typed API return types for OpenAPI client generation.
- Treat generated clients as build artifacts: regenerate after contract changes; do not hand-edit generated code.
