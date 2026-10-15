You are working in module `<MODULE_PATH>` of the .NET `<Project>` solution.

Generate 3 documents for AI agents:

- `README.md` (English)
- `AGENT_CONTEXT.md` (English)
- `DEPENDENCY.md` (English)

Goal: provide a fast, accurate, and safe editing context for this module using only facts from code.

General rules:

1. Write only from code and `.csproj` facts.
2. If docs and code differ, capture it as `Known Debt / Sharp Edge`.
3. Do not copy large code blocks; only minimal signatures/examples.
4. Tone: concise, technical, factual.
5. Documentation only; no code changes.

## README.md

- What the module does (1–2 sentences).
- Scope: what it does and what it does NOT do.
- Public contract (interfaces/primary methods).
- Integration (DI registration / host wiring).
- Required configuration (section keys + example JSON).
- Important behavior notes (invariants, logging, lifecycle).
- Key source files list.
- Links to AGENT_CONTEXT.md and DEPENDENCY.md.

## AGENT_CONTEXT.md (English)

- Module Purpose.
- Source Of Truth priority.
- Current Runtime Wiring (DI method, config sections, lifetimes).
- External Call Sites (High Impact).
- Behavior Invariants.
- Known Debt / Sharp Edges.
- Editing Rules For Agents (forbidden without request; sync requirements).
- Minimal Verification After Changes (build/test commands).

## DEPENDENCY.md (English)

- Framework details (TFM, nullable, etc.).
- NuGet packages (purpose of each).
- Internal project references (why each is needed).
- Architectural Dependency Rules (allowed / forbidden directions).
- Contract Stability (what AI must not modify unless required).
- Runtime dependencies (config keys, external systems).
- External System Boundary.
- Coupling / Change Impact.
- Safe Change Checklist.

Output: create or update exactly these 3 files in the target module folder; end with a short report of risks/discrepancies.
