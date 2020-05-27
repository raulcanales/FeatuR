#!/bin/bash
echo Executing after success scripts on branch $TRAVIS_BRANCH
echo Triggering Nuget package build

VERSION=$(git tag)

cd src/FeatuR/src/FeatuR
dotnet pack -c release /p:PackageVersion=$VERSION-preview-$TRAVIS_BUILD_NUMBER --no-restore -o .

echo "Pushing package to MyGet"

case "$TRAVIS_BRANCH" in
  "master")
    dotnet nuget push *.nupkg -k $MYGET_API_KEY -Source https://www.myget.org/F/featur/api/v2/package
    ;;
esac