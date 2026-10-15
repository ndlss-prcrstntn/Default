# DEPENDENCY

## Framework
- TargetFramework: 
net10.0
- Nullable: 
enable
- ImplicitUsings: 
enable


## NuGet Packages
- AutoMapper (16.0.0)

## Project References
- None

## Consumers
- Default.DependencyInjection
- Default.Hosting
- Default.Names
- Default.Scheduler

## Dependency Rules
- Allowed: directions declared in ProjectReference.
- Forbidden: circular or reverse host coupling without decision.

## Contract Stability
- Public types and extension methods are high-impact.
