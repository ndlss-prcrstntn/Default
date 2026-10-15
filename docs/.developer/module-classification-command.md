Create or update: `/docs/architecture/module-classification.md`

Scan all `.csproj` under `src/` and classify each project.

For each project provide:

| Project | Type | Responsibility | Notes |

Allowed types:

- Host Application
- Domain/Feature Module
- Infrastructure Module
- Shared Kernel / Shared Types / Abstractions
- Data Access Support
- Client Application

Also:

- Summarize system shape.
- List explicit transitional zones inside the host.
- Include a Boundary Warning: folder ≠ module boundary; decide by references and ownership.

Write facts only. Call out name collisions / dual projects (e.g. same capability in Modules and Infrastructure).
