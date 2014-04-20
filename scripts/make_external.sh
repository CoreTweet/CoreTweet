#!/bin/sh
git submodule update --init --recursive
cd ExternalDependencies/doxygen
./configure || {
    echo 'error: while runnning configure' ;
    exit 1; 
}
make || {
    echo 'error: while runnning make' ;
    exit 1; 
}
