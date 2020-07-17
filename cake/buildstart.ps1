[CmdletBinding()]
Param(
    [Parameter(Mandatory=$false)]
    [String]$target="Default"
)

dotnet --version
Write-Host $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath('.\')
Write-Host "Cake: installing cake addons,tools,modules"
./build.ps1 -Target="Bootstrap" --bootstrap
Write-Host "Cake: running build task: " $target
./build.ps1 -Target $target -Verbosity="Normal" 
