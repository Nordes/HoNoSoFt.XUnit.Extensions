############### Create and push a nuget package   #########################
#--------------------------------------------------------------------------
###########################################################################
[CmdletBinding()]
Param(
  [Parameter(Mandatory=$false)]
  [string]$k = "somekindofkey",

  # Set the version
  [Parameter(Mandatory=$false)]
  [string]$v = "1.0.0",

  # Show help
  [Parameter(Mandatory=$false)]
  [switch]$h = $false
)

if ($h -or $key -eq "somekindofkey" ){
  Write-Host "==============================="
  Write-Host "Parameters:"
  Write-Host " -h                 : This documentation"
  Write-Host " -v                 : Nuget version of package"
  Write-Host " -k thenugetkey     : Use that key to publish the package"
  Write-Host "==============================="
  exit 0;
}

$nupkgs = "$((Get-Location).Path)\nupkgs"
# Remove all the files from the nupkgs folder
Get-ChildItem "$((Get-Location).Path)\nupkgs" | ForEach-Object { $_.Delete() }

$cmd = "New-Item -ItemType directory -Path $nupkgs"
Invoke-Expression $cmd

$cmd = "dotnet build ./HoNoSoFt.XUnit.Extensions/HoNoSoFt.XUnit.Extensions.csproj -c=Release"
Invoke-Expression $cmd

# Build and pack
$cmd = "dotnet pack ./HoNoSoFt.XUnit.Extensions/HoNoSoFt.XUnit.Extensions.csproj /p:PackageVersion=$v --include-source --include-symbols -c=Release -o $nupkgs"
Invoke-Expression $cmd

# Ask if we really want to push the packages.
$cmd = "dotnet nuget push $nupkgs\HoNoSoFt.XUnit.Extensions.$v.nupkg -s=https://api.nuget.org/v3/index.json -ss=https://nuget.smbsrc.net -k=$k --force-english-output"
Invoke-Expression $cmd