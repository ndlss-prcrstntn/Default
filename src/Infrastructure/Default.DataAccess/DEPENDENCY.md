# DEPENDENCY

## Framework
- TargetFramework: 
net10.0
- Nullable: 
not set
- ImplicitUsings: 
enable


## NuGet Packages
- FlexLabs.EntityFrameworkCore.Upsert (10.0.0)
- Microsoft.EntityFrameworkCore (10.0.3)
- Microsoft.EntityFrameworkCore.Analyzers (10.0.3)
- Microsoft.EntityFrameworkCore.Design (10.0.3)
- Microsoft.EntityFrameworkCore.Proxies (10.0.3)
- Microsoft.EntityFrameworkCore.Relational (10.0.3)
- Microsoft.Extensions.Hosting.Abstractions (10.0.3)
- Npgsql.EntityFrameworkCore.PostgreSQL (10.0.0)

## Project References
- Default.DependencyInjection

## Consumers
- Default.Api
- Default.DataAccess.EntityFramework
- Default.DataAccess.Models
- Default.Scheduler

## Dependency Rules
- Allowed: directions declared in ProjectReference.
- Forbidden: circular or reverse host coupling without decision.

## Contract Stability
- Public types and extension methods are high-impact.
