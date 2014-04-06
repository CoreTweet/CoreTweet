#!/bin/sh
cd Binary/Nightly
rm -rf docs*
cd ../..
ExternalDependencies/doxygen/bin/doxygen
