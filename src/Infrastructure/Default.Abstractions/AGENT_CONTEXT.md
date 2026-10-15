# AGENT_CONTEXT

## Module Purpose
Default.Abstractions
 is a 
Infrastructure Module
.

## Source Of Truth
1. 
Default.Abstractions.csproj
2. DI/config files in this project
3. Public contracts in source code

## Runtime Wiring
- TargetFramework: 
net10.0
- Nullable: 
enable
- ImplicitUsings: 
enable


## External Call Sites
- Consumers: 
Default.Cache, Default.DependencyInjection, Default.Ext, Default.FileStorage, Default.Hosting, Default.Mail, Default.Scheduler


## Invariants
- Keep project boundary of its category.
- Avoid breaking public contracts without syncing consumers.

## Editing Rules
- MUST NOT create circular references.
- MUST NOT move unrelated layer logic here.

## Verification
- dotnet build src/Default.sln
