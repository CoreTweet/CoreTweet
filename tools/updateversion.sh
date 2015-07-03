#!/bin/bash
find . \( -name "*.yml" -o -name "*.cs" -o -name "*.fs" -o -name "*.nuspec" -o -name "*file" \) -exec sed -i "s/$1/$2/g" {} \; 
