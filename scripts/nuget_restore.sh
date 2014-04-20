#!/bin/sh
for i in CoreTweet*/
do
  cd $i
  mono ../ExternalDependencies/nuget/NuGet.exe restore -ConfigFile packages.config -PackagesDirectory ../packages
  cd ..
done
