# DEPENDENCY

## Framework
- TargetFramework: 
net10.0
- Nullable: 
enable
- ImplicitUsings: 
enable


## NuGet Packages
- Hangfire.AspNetCore (1.8.23)
- Hangfire.PostgreSql (1.21.1)

## Project References
- Default.DataAccess
- Default.Hosting

## Consumers
- None

## Dependency Rules
- Allowed: directions declared in ProjectReference.
- Forbidden: circular or reverse host coupling without decision.

## Contract Stability
- Public types and extension methods are high-impact.
