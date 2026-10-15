# Default.Api — Change History

## Entries

### 2026-10-15 — Skeleton host

- Composition root wires infrastructure (logs, data access, mail, S3, JWT, CORS, SignalR, Hangfire).
- No domain modules yet — register them from `AddAppServices`.
