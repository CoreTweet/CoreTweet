<#
    .SYNOPSIS 
      Build CoreTweet
    .EXAMPLE
     build -All
     build -WithPcl -Binary
#>

param (
    [switch]$Force32bit,
    [switch]$All,
    [switch]$Binary,
    [switch]$Docs,
    [switch]$Package,
    [switch]$Clean,
    [switch]$WithPcl,
    [switch]$Help
)

if($Help)
{
   echo "Usage: build.ps1 [-WithPcl] -All | -Binary | -Docs | -Package | -Clean"
   echo ""
   echo "Targets:"
   echo "    All      ... Build binaries, docs, and packages"
   echo "    Binary   ... Build binaries only"
   echo "    Docs     ... Build documents only"
   echo "    Packages ... Build nupkgs only"
   echo "    Clean    ... Clean generated files"
   echo ""
   echo "Options:"
   echo "    WithPcl  ... Build binaries for Windows Platforms"
   exit
}

if(!($Binary -or $Docs -or $Package -or $Clean))
{
   $All = $true
}


# Set .NET's current directory to here

$fullpath = $MyInvocation.MyCommand.Definition
$this = $MyInvocation.MyCommand.Name
[System.IO.Directory]::SetCurrentDirectory($fullpath.Replace($this, ""))

$doxygen = ".\ExternalDependencies\bin\doxygen.exe"
$doxygen_url = "http://ftp.stack.nl/pub/users/dimitri/doxygen-1.8.7.windows.bin.zip"
$doxygen_zip = ".\ExternalDependencies\doxygen.zip"

$nuget = ".\ExternalDependencies\bin\nuget.exe"
$nuget_url = "http://nuget.org/nuget.exe"

If(!(Test-Path $doxygen_zip) -and !(Test-Path $doxygen))
{
  echo "Downloading Doxygen..."
  $wc = new-object System.Net.WebClient
  $wc.DownloadFile($doxygen_url, $doxygen_zip)

  echo "Extracting..."
  [System.Reflection.Assembly]::LoadWithPartialName('System.IO.Compression.FileSystem')
  [System.IO.Compression.ZipFile]::ExtractToDirectory($doxygen_zip, ".\ExternalDependencies\bin")
  rm $doxygen_zip
}

If(!(Test-Path $nuget))
{
  echo "Downloading NuGet..."
  mkdir -Force .\ExternalDependencies\bin
  $wc = new-object System.Net.WebClient
  $wc.DownloadFile($nuget_url, $nuget)
}

if($Clean)
{
  rm -Recurse -Force .\Binary
}

if($All -or $Binary)
{
  if(!(Test-Path packages/))
  {
    echo "Installing NuGet packages..."
    foreach ($path in Get-ChildItem CoreTweet*)
    {
      if($path.Attributes -eq "Directory")
      {
        cd $path
        & ../$nuget restore -ConfigFile packages.config -PackagesDirectory ../packages
        cd ..
      }
    }
  }

  $is32bit = [IntPtr]::Size -eq 4

  if($WithPcl)
  {
    echo "Use VisualStudio's MSBuild."
    if($is32bit)
    {
      $msbuild = "C:\Program Files\MSBuild\12.0\Bin\MSBuild.exe"
    }
    else
    {
      $msbuild = "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe"
    }
    & $msbuild /m /p:Configuration=Release .\CoreTweet-All.sln
  }
  else
  {
    if($is32bit -or $Force32bit -eq $true)
    {
      echo "Use 32bit MSBuild."
      $msbuild = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
    }
    else
    {
      echo "Use 64bit MSBuild."
      $msbuild = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
    }
    & $msbuild /m /p:Configuration=Release .\CoreTweet-Mono.sln
  }
}

if($All -or $Docs)
{
  & $doxygen
}

if($All -or $Package)
{
  If($WithPcl)
  {
    cp -Force .\nuspecs\CoreTweet.nuspec .\Binary\Nightly\CoreTweet.nuspec
    cp -Force .\nuspecs\CoreTweet.Streaming.Reactive.nuspec .\Binary\Nightly\CoreTweet.Streaming.Reactive.nuspec
  }
  Else
  {
    cp -Force .\nuspecs\CoreTweet-Mono.nuspec .\Binary\Nightly\CoreTweet.nuspec
    cp -Force .\nuspecs\CoreTweet.Streaming.Reactive-Mono.nuspec .\Binary\Nightly\CoreTweet.Streaming.Reactive.nuspec
  }
  & $nuget pack .\Binary\Nightly\CoreTweet.nuspec -OutputDirectory .\Binary\Nightly
  & $nuget pack .\Binary\Nightly\CoreTweet.Streaming.Reactive.nuspec -OutputDirectory .\Binary\Nightly
}
