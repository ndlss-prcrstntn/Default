# AGENT_CONTEXT

## Module Purpose
Default.SignalR
 is a 
Infrastructure Module
.

## Source Of Truth
1. 
Default.SignalR.csproj
2. DI/config files in this project
3. Public contracts in source code

## Runtime Wiring
- TargetFramework: 
net8.0
- Nullable: 
enable
- ImplicitUsings: 
enable


## External Call Sites
- Consumers: 
None


## Invariants
- Keep project boundary of its category.
- Avoid breaking public contracts without syncing consumers.

## Editing Rules
- MUST NOT create circular references.
- MUST NOT move unrelated layer logic here.

## Verification
- dotnet build src/Default.sln
