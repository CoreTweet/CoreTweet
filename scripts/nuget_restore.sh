#!/bin/sh
for i in CoreTweet*/
do
  cd $i
  mono ../ExternalDependencies/nuget/src/CommandLine/bin/Release/NuGet.exe restore -ConfigFile packages.config -PackagesDirectory ../packages
  cd ..
done
