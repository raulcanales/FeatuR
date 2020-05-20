#!/usr/bin/env bash
#exit if any command fails
set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then
  rm -R $artifactsFolder
fi

dotnet restore ./FeatuR.sln
dotnet test ./test/FeatuR.Tests/FeatuR.Tests.csproj -c Release
dotnet build -c Release