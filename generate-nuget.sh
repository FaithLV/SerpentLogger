#!/bin/sh
dotnet pack --include-source --no-build --include-symbols -c Release --output nupkgs

mkdir bin/nupkgs

# Delete previous package builds
rm bin/nupkgs/*

# Copy latests packages 
cp SerpentAPI/nupkgs/* bin/nupkgs -v
cp SerpentKernel/nupkgs/* bin/nupkgs -v
cp SerpentLogger/nupkgs/* bin/nupkgs -v

# Clear build artifacts
rm SerpentAPI/nupkgs/*
rm SerpentKernel/nupkgs/* 
rm SerpentLogger/nupkgs/*
