# DEPENDENCY

## Framework
- TargetFramework: 
net10.0
- Nullable: 
not set
- ImplicitUsings: 
enable


## NuGet Packages
- Castle.Core (5.2.1)
- Microsoft.Extensions.Options (10.0.3)

## Project References
- Default.Abstractions

## Consumers
- Default.Api

## Dependency Rules
- Allowed: directions declared in ProjectReference.
- Forbidden: circular or reverse host coupling without decision.

## Contract Stability
- Public types and extension methods are high-impact.
