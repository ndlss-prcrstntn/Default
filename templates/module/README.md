# <Project.ModuleName>

Brief summary of what the module does (1–2 sentences).

## Scope

**Does:**

- …

**Does not:**

- … (explicit responsibility boundaries)

## Public contract

- `I…` — …
- Main operations: …

## Integration

- DI: `services.Add…Module(...)` (or the actual extension method name)
- Wired from: `<Host.Api>` / …

## Configuration

```json
{
  "SectionName": {
    "Key": "value"
  }
}
```

## Behavior notes

- Invariants, lifecycle, logging — keep this short.

## Key files

- `…`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
