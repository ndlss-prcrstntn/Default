# Transitional and Legacy Zones in `<Host.Api>`

## Purpose

Describe real transitional/legacy zones so agents evolve the system safely without expanding host-owned business logic.

This is a legacy-tolerant description of the **current state**, not a target-state fantasy.

## Definition: Transitional Zone

A **Transitional Zone** is a host-local code area that currently contains mixed concerns (transport + orchestration + partial business behavior) because extraction into modules is incomplete.

A zone is transitional when one or more of the following are true:

- It coordinates multiple services and cross-cutting concerns from the host layer.
- It contains business-adjacent decision branches that should eventually live behind module contracts.
- It exists for backward compatibility or incremental migration.

## Explicit Transitional Zones

Fill from the repository. Common candidates:

| Zone | Why transitional | Migration intent |
| --- | --- | --- |
| `DependencyInjection/` | composition can grow accidental behavior | keep composition-only |
| `Startup/` | seed/boot policies may encode business rules | push policies to modules |
| `DataAccess/Migrations/` | persistence artifacts co-located with host | keep stable; avoid expanding host persistence logic |
| Mapping profiles | mapping can encode business decisions | keep mechanical; move rules to modules |
| Host “application services” | orchestration + rules mixed | extract behind module contracts |

## AI Agent Rules for Transitional Zones

1. Prefer extraction to modules.
2. Do not add new core business logic to transitional host areas.
3. Allow stabilization: bug fixes, reliability, observability, tests.
4. Refactor safely: preserve behavior first, then extract.
5. Keep host focused on transport, composition, and orchestration boundaries.
6. If temporary host logic is unavoidable, mark it transitional and design for near-term extraction.

## Evolution Strategy

### Strangler Pattern

- Introduce module-owned contracts for one use case at a time.
- Route new or changed behavior through module contracts first.
- Remove old host paths only after parity is verified.

### Gradual Modularization

- Migrate by vertical slices.
- Reduce each host slice to endpoint + DTO translation + delegation.

### Behavior-Preserving Refactors

- Establish current behavior with tests before extraction.
- Keep API contracts and side effects identical during migration steps unless explicitly planned.
