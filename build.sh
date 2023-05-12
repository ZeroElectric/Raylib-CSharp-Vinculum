#!/bin/bash

echo "Building raylib..."

cd Build/

./premake5 gmake2

make config=release_x64 clean

make config=release_x64

echo "Building Complete!"
