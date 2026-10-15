# DEPENDENCY

## Framework
- TargetFramework: 
net10.0
- Nullable: 
enable
- ImplicitUsings: 
enable


## NuGet Packages
- AWSSDK.Extensions.NETCore.Setup (4.0.3.23)
- AWSSDK.S3 (4.0.18.7)
- Microsoft.Extensions.Options (10.0.3)

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
