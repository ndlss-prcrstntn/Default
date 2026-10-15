# AGENT_CONTEXT

## Module Purpose
Default.Cache
 is a 
Infrastructure Module
.

## Source Of Truth
1. 
Default.Cache.csproj
2. DI/config files in this project
3. Public contracts in source code

## Runtime Wiring
- TargetFramework: 
net10.0
- Nullable: 
not set
- ImplicitUsings: 
enable


## External Call Sites
- Consumers: 
Default.Web


## Invariants
- Keep project boundary of its category.
- Avoid breaking public contracts without syncing consumers.

## Editing Rules
- MUST NOT create circular references.
- MUST NOT move unrelated layer logic here.

## Verification
- dotnet build src/Default.sln
