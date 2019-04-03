#!/bin/sh
dotnet test  \
	SerpentTests/SerpentTests.csproj \
	/p:CollectCoverage=true \
	/p:Exclude=\"[xunit.*]*\" \
	/p:CoverletOutputFormat=\"opencover,lcov\" \
	/p:CoverletOutput=../lcov
