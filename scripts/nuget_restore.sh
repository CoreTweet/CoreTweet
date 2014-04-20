#!/bin/sh
for i in CoreTweet*/
do
  cd $i
  ../ExternalDependencies/nuget/bin/nuget restore -ConfigFile packages.config -PackagesDirectory ../packages
  cd ..
done
