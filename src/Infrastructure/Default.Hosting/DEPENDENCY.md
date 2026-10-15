# DEPENDENCY

## Framework
- TargetFramework: 
net10.0
- Nullable: 
enable
- ImplicitUsings: 
enable


## NuGet Packages
- Microsoft.AspNetCore.Hosting.Abstractions (2.3.9)
- Microsoft.AspNetCore.Http.Abstractions (2.3.9)
- Microsoft.Extensions.Configuration (10.0.3)
- Microsoft.Extensions.Configuration.EnvironmentVariables (10.0.3)
- Microsoft.Extensions.Configuration.Json (10.0.3)
- Microsoft.Extensions.Configuration.UserSecrets (10.0.3)
- Microsoft.Extensions.Hosting.Abstractions (10.0.3)
- Scrutor (7.0.0)
- Serilog (4.3.1)

## Project References
- Default.Abstractions
- Default.Syntax

## Consumers
- Default.Logs
- Default.Scheduler
- Default.Security

## Dependency Rules
- Allowed: directions declared in ProjectReference.
- Forbidden: circular or reverse host coupling without decision.

## Contract Stability
- Public types and extension methods are high-impact.
