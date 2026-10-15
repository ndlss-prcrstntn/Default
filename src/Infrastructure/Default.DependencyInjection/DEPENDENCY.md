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
- Microsoft.Extensions.Configuration (10.0.3)
- Microsoft.Extensions.Configuration.Binder (10.0.3)
- Microsoft.Extensions.DependencyInjection (10.0.3)
- Microsoft.Extensions.DependencyInjection.Abstractions (10.0.3)
- Microsoft.Extensions.Options (10.0.3)
- Microsoft.Extensions.Options.ConfigurationExtensions (10.0.3)

## Project References
- Default.Abstractions
- Default.Syntax

## Consumers
- Default.Cache
- Default.DataAccess
- Default.FileStorage
- Default.Logs
- Default.Mail

## Dependency Rules
- Allowed: directions declared in ProjectReference.
- Forbidden: circular or reverse host coupling without decision.

## Contract Stability
- Public types and extension methods are high-impact.
