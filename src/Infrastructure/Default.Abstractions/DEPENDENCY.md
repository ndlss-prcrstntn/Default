# DEPENDENCY

## Framework
- TargetFramework: 
net10.0
- Nullable: 
enable
- ImplicitUsings: 
enable


## NuGet Packages
- None

## Project References
- None

## Consumers
- Default.Cache
- Default.DependencyInjection
- Default.Ext
- Default.FileStorage
- Default.Hosting
- Default.Mail
- Default.Scheduler

## Dependency Rules
- Allowed: directions declared in ProjectReference.
- Forbidden: circular or reverse host coupling without decision.

## Contract Stability
- Public types and extension methods are high-impact.
