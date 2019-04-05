#!/bin/sh
dotnet publish SerpentLogger -c Release
mkdir bin
tar -cvzf bin/SerpentKernel.tar.gz SerpentKernel/bin/Release/netstandard2.0/*
tar -cvzf bin/SerpentLogger.tar.gz SerpentLogger/bin/Release/netstandard2.0/publish/*
