# DEPENDENCY

## Framework
- TargetFramework: 
net10.0
- Nullable: 
enable
- ImplicitUsings: 
enable


## NuGet Packages
- MailKit (4.15.0)
- Microsoft.Extensions.Configuration.Abstractions (10.0.3)
- Microsoft.Extensions.Logging.Abstractions (10.0.3)
- Microsoft.Extensions.Options (10.0.3)
- MimeKit (4.15.0)

## Project References
- Default.Abstractions
- Default.DependencyInjection

## Consumers
- Default.Api

## Dependency Rules
- Allowed: directions declared in ProjectReference.
- Forbidden: circular or reverse host coupling without decision.

## Contract Stability
- Public types and extension methods are high-impact.
