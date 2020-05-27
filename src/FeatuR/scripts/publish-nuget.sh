#!/bin/bash
echo Executing after success scripts on branch $TRAVIS_BRANCH
echo Triggering Nuget package build

VERSION=$(git tag)

cd src/FeatuR/src/FeatuR
dotnet pack -c release /p:PackageVersion=$VERSION-preview-$TRAVIS_BUILD_NUMBER --no-restore -o .

case "$TRAVIS_BRANCH" in
  "master")
    dotnet nuget push *.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
    ;;
esac