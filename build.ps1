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
    echo "    ExecuteTemplate ... Generate RestApis.cs"
    exit
}

if(!($Binary -or $Docs -or $Package -or $Clean -or $ExecuteTemplate))
{
    $All = $true
}

$solution = ".\CoreTweet-All.sln"
mkdir .\Release -Force > $null

$nuget = ".\ExternalDependencies\bin\nuget.exe"
$nuget_url = "https://dist.nuget.org/win-x86-commandline/v5.5.1/nuget.exe"

function Download-NuGet
{
    if(!(Test-Path $nuget))
    {
        echo "Downloading NuGet..."
        mkdir -Force (Split-Path $nuget -Parent) > $null
        Invoke-WebRequest $nuget_url -OutFile $nuget
    }
}

$vssetup_zip = ".\ExternalDependencies\bin\VSSetup.zip"
$vssetup_url = "https://github.com/Microsoft/vssetup.powershell/releases/download/2.2.16/VSSetup.zip"
$vssetup_module = ".\ExternalDependencies\bin\VSSetup\VSSetup.psd1"

function Require-MSBuild
{
    if($script:msbuild -eq $null)
    {
        if(!(Test-Path $vssetup_module))
        {
            Write-Host "Downloading VSSetup..."
            mkdir -Force (Split-Path $vssetup_zip -Parent) > $null
            Invoke-WebRequest $vssetup_url -OutFile $vssetup_zip
            Expand-Archive $vssetup_zip (Split-Path $vssetup_module -Parent)
        }
        Import-Module $vssetup_module

        $vs_instance = Get-VSSetupInstance -All | Select-VSSetupInstance -Require ('Microsoft.Build', 'Microsoft.VisualStudio.Component.Roslyn.Compiler') -Latest

        if($vs_instance -eq $null)
        {
            Write-Host "Couldn't find Visual Studio 2017"
            exit 1
        }

        $script:msbuild = Join-Path $vs_instance.InstallationPath "MSBuild\15.0\Bin\MSBuild.exe"

        if (!(Test-Path $script:msbuild)) {
            # for Visual Studio 2019
            $script:msbuild = Join-Path $vs_instance.InstallationPath "MSBuild\Current\Bin\MSBuild.exe"
        }
    }
}

if($Clean)
{
    Require-MSBuild
    & $msbuild $solution /m /target:Clean
    rm -Recurse -Force .\Release
    rm CoreTweet.Shared\RestApis.cs
}

if($All -or $ExecuteTemplate -or $Binary)
{
    Require-MSBuild
    & $msbuild RestApisGen\RestApisGen.csproj /p:Configuration=Debug
    RestApisGen\bin\RestApisGen.exe
}

if($All -or $Binary)
{
    Require-MSBuild
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
