#!/bin/sh

# Taken from https://github.com/ngenerics/ngenerics
# Only need Mono and Sonar for non-pull request builds
if [ "$TRAVIS_PULL_REQUEST" = "false" ]; then
	# Install a more recent version of Mono
	sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
	echo "deb http://download.mono-project.com/repo/ubuntu trusty main" | sudo tee /etc/apt/sources.list.d/mono-official.list
	echo "Installing build tools..."

	sudo apt-get -qq update
	sudo apt-get -qq install -y mono-complete unzip wget

	# Install the MSBuild Sonar scanner
	wget -O sonar.zip https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/4.9.0.17385/sonar-scanner-msbuild-4.9.0.17385-netcoreapp3.0.zip
	unzip -qq sonar.zip -d tools/sonar
	ls -l tools/sonar
	chmod +x tools/sonar/sonar-scanner-4.3.0.2102/bin/sonar-scanner
fi