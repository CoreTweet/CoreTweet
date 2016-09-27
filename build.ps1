<#
    .SYNOPSIS
      Build CoreTweet
    .EXAMPLE
     build -All
     build -Binary
#>

param (
    [switch]$All,
    [switch]$Binary,
    [switch]$Docs,
    [switch]$Package,
    [switch]$Clean,
    [switch]$ExecuteTemplate,
    [switch]$Help
)

if($Help)
{
    echo "Usage: build.ps1 -All | -Binary | -Docs | -Package | -Clean | -ExecuteTemplate"
    echo ""
    echo "Targets:"
    echo "    All      ... Build binaries, docs, and packages"
    echo "    Binary   ... Build binaries only"
    echo "    Docs     ... Build documents only"
    echo "    Packages ... Build nupkgs only"
    echo "    Clean    ... Clean generated files"
    echo "    ExecuteTemplete ... Generate RestApis.cs"
    exit
}

if(!($Binary -or $Docs -or $Package -or $Clean -or $ExecuteTemplate))
{
    $All = $true
}

$solution = ".\CoreTweet-All.sln"
mkdir .\Release -Force > $null

$nuget = ".\ExternalDependencies\bin\nuget.exe"
$nuget_url = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"

function Download-NuGet
{
    if(!(Test-Path $nuget))
    {
        echo "Downloading NuGet..."
        mkdir -Force .\ExternalDependencies\bin > $null
        Invoke-WebRequest $nuget_url -OutFile $nuget
    }
}

if([Environment]::Is64BitOperatingSystem)
{
    $msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"
}
else
{
    $msbuild = "C:\Program Files\MSBuild\14.0\Bin\MSBuild.exe"
}

if($Clean)
{
    & $msbuild $solution /m /target:Clean
    rm -Recurse -Force .\Release
    rm CoreTweet.Shared\RestApis.cs
}

if($All -or $ExecuteTemplate -or $Binary)
{
    & $msbuild RestApisGen\RestApisGen.csproj /p:Configuration=Debug
    RestApisGen\bin\RestApisGen.exe
}

if($All -or $Binary)
{
    Download-NuGet
    & $nuget restore $solution
    & $msbuild $solution /m /p:Configuration=Release
}

if($All -or $Docs)
{
    try
    {
        Get-Command doxygen -ErrorAction Stop > $null
        $existsDoxygen = $true
    }
    catch [System.Management.Automation.CommandNotFoundException]
    {
        echo "Doxygen is not installed"
        $existsDoxygen = $false
    }
    if($existsDoxygen) { doxygen }
}

if($All -or $Package)
{
    $version = $env:APPVEYOR_BUILD_VERSION
    if($version -eq $null)
    {
        $version = (Get-Item .\Release\net40\CoreTweet.dll).VersionInfo.ProductVersion
    }

    Download-NuGet
    & $nuget pack CoreTweet.nuspec -Version $version -OutputDirectory .\Release

    if($env:APPVEYOR_REPO_BRANCH -eq "master")
    {
        Get-ChildItem .\Release\*.nupkg |
            foreach { Push-AppveyorArtifact $_.FullName -FileName $_.Name }
    }
}
