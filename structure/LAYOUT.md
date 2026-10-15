# Repository layout

Canonical shape for a hybrid modular monolith. Folder names are conventions; **`.csproj` boundaries are architectural units**.

```text
.
├── AGENTS.md
├── README.md
├── .cursor/
│   └── rules/                    # always-on agent rules
├── .specify/
│   ├── memory/constitution.md    # governance principles
│   └── templates/                # feature/spec templates
├── docs/
│   ├── architecture/             # system truth
│   └── .developer/               # prompts to (re)generate architecture docs
├── scripts/
│   └── add-migration.ps1
├── src/
│   ├── VERSION                   # semver source of truth
│   ├── Directory.Build.props     # wires VERSION into assemblies
│   ├── Default.slnx
│   ├── Default.Api/              # composition root
│   │   ├── CHANGELOG.md
│   │   ├── README.md
│   │   └── DataAccess/Migrations/
│   ├── Default.Client/           # Ionic Angular skeleton
│   │   └── CHANGELOG.md
│   ├── Modules/                  # domain modules (empty starter)
│   │   └── Default.FeatureX/     # copy templates/module
│   └── Infrastructure/
│       ├── Default.Abstractions/
│       ├── Default.Cache/
│       ├── Default.DataAccess/
│       ├── Default.DependencyInjection/
│       ├── Default.Ext/
│       ├── Default.FileStorage/
│       ├── Default.Hosting/
│       ├── Default.Logs/
│       ├── Default.Mail/
│       ├── Default.Scheduler/
│       ├── Default.SignalR/
│       ├── Default.Syntax/
│       └── Default.Web/
└── tests/
    ├── Modules/
    ├── Integration/
    └── Infrastructure/
```

## Dependency direction

```text
Default.Client  →  Default.Api  →  Modules/*  →  Infrastructure/Default.*
```

- Modules MUST NOT reference `Default.Api`.
- Host MAY orchestrate modules via public contracts.
- Cross-module calls go through approved contracts, not host shortcuts.

## Module documentation triad

Every extractable project should have:

| File | Language | Role |
| --- | --- | --- |
| `README.md` | human (team language) | what / scope / integration |
| `AGENT_CONTEXT.md` | English | invariants, wiring, edit rules |
| `DEPENDENCY.md` | English | packages, refs, coupling |
| `CHANGELOG.md` | team language | substantive history |

Copy starters from `templates/module/`.
