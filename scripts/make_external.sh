#!/bin/sh
git submodule update --init --recursive
cd ExternalDependencies/doxygen
./configure
make
cd ../nuget
./build.sh
