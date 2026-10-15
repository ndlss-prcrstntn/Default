# Module CHANGELOG maintenance

When finishing substantive work in a module, update that module’s `CHANGELOG.md`.

## Location

- `src/Modules/<Module>/CHANGELOG.md`
- `src/Infrastructure/<Library>/CHANGELOG.md`
- `src/<Host.Api>/CHANGELOG.md`
- `src/<Client>/CHANGELOG.md`

## Entry format

```markdown
### YYYY-MM-DD — Short title

- Core behavior / contract change (1–5 bullets).
- Skip file lists and formatting-only noise.
```

Newest entries go under `## Entries`, above older ones.

## Related docs

- `AGENT_CONTEXT.md` / `README.md` — current module truth (update if invariants changed).
- `CHANGELOG.md` — chronological history only.
- `src/VERSION` — still bump per `.cursor/rules/app-versioning.mdc`.
