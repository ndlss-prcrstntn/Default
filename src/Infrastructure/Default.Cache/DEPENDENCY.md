# DEPENDENCY

## Framework
- TargetFramework: 
net10.0
- Nullable: 
not set
- ImplicitUsings: 
enable


## NuGet Packages
- Microsoft.Extensions.Configuration.Abstractions (10.0.3)
- Microsoft.Extensions.DependencyInjection.Abstractions (10.0.3)
- Newtonsoft.Json (13.0.4)
- StackExchange.Redis (2.11.8)

## Project References
- Default.Abstractions
- Default.DependencyInjection

## Consumers
- Default.Web

## Dependency Rules
- Allowed: directions declared in ProjectReference.
- Forbidden: circular or reverse host coupling without decision.

## Contract Stability
- Public types and extension methods are high-impact.
