#!/bin/sh
git submodule update --init --recursive
cd ExternalDependencies/nuget
./build.sh
cd ../..
for i in CoreTweet*/
do
  cd $i
  mono ../ExternalDependencies/nuget/src/CommandLine/bin/Release/NuGet.exe restore -ConfigFile packages.config -PackagesDirectory ../packages
  cd ..
done
