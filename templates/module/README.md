# <Project.ModuleName>

Кратко: что делает модуль (1–2 предложения).

## Scope

**Делает:**

- …

**Не делает:**

- … (явные границы ответственности)

## Public contract

- `I…` — …
- Основные операции: …

## Integration

- DI: `services.Add…Module(...)` (или фактический метод)
- Подключается из: `<Host.Api>` / …

## Configuration

```json
{
  "SectionName": {
    "Key": "value"
  }
}
```

## Behavior notes

- Инварианты / lifecycle / логирование — кратко.

## Key files

- `…`

## See also

- [AGENT_CONTEXT.md](./AGENT_CONTEXT.md)
- [DEPENDENCY.md](./DEPENDENCY.md)
- [CHANGELOG.md](./CHANGELOG.md)
