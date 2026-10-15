# DEPENDENCY

## Framework
- TargetFramework: 
net8.0
- Nullable: 
enable
- ImplicitUsings: 
enable


## NuGet Packages
- Microsoft.AspNetCore.SignalR (1.2.0)
- Microsoft.AspNetCore.SignalR.Client (8.0.14)
- Microsoft.Extensions.DependencyInjection (8.0.1)
- Microsoft.Extensions.DependencyInjection.Abstractions (8.0.2)

## Project References
- None

## Consumers
- None

## Dependency Rules
- Allowed: directions declared in ProjectReference.
- Forbidden: circular or reverse host coupling without decision.

## Contract Stability
- Public types and extension methods are high-impact.
