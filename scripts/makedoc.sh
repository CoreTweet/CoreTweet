#!/bin/sh

if hash "doxygen" 2> /dev/null; then
  doxygen
else
  ExternalDependencies/doxygen/bin/doxygen
fi
