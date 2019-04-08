#!/bin/sh
dotnet pack --include-source --include-symbols -c Release --output nupkgs
mkdir bin/nupkgs
cp SerpentAPI/nupkgs/* bin/nupkgs -v
cp SerpentKernel/nupkgs/* bin/nupkgs -v
cp SerpentLogger/nupkgs/* bin/nupkgs -v
