<#
.SYNOPSIS
  Create an EF Core migration the only supported way (generates .cs + Designer + snapshot).

.EXAMPLE
  .\scripts\add-migration.ps1 AddSomething
#>
param(
    [Parameter(Mandatory = $true, Position = 0)]
    [string] $Name
)

$ErrorActionPreference = 'Stop'
$repoRoot = Resolve-Path (Join-Path $PSScriptRoot '..')
$HostProject = Join-Path $repoRoot 'src/Default.Api/Default.Api.csproj'
$MigrationsRelativeDir = 'DataAccess/Migrations'

if (-not (Test-Path $HostProject)) {
    throw "Host project not found at $HostProject"
}

Push-Location $repoRoot
try {
    dotnet ef migrations add $Name `
        --project $HostProject `
        --output-dir $MigrationsRelativeDir

    $hostDir = Split-Path $HostProject -Parent
    $migrationsDir = Join-Path $hostDir $MigrationsRelativeDir
    $main = Get-ChildItem $migrationsDir -Filter "*_$Name.cs" |
        Where-Object { $_.Name -notlike '*.Designer.cs' } |
        Sort-Object LastWriteTime -Descending |
        Select-Object -First 1
    $designer = if ($main) { Join-Path $migrationsDir ($main.BaseName + '.Designer.cs') } else { $null }

    if (-not $main -or -not (Test-Path $designer)) {
        throw "Migration created but Designer missing. Do not commit; fix with 'dotnet ef migrations remove' and retry."
    }

    Write-Host "OK: $($main.Name) + $([IO.Path]::GetFileName($designer))"
}
finally {
    Pop-Location
}
