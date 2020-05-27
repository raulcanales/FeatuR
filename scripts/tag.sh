if [[ "$OSTYPE" == "darwin"* ]]; then
  # from https://stackoverflow.com/questions/3572030/bash-script-absolute-path-with-os-x
  function realpath(){
    [[ $1 = /* ]] && echo "$1" || echo "$PWD/${1#./}"
  }  
fi

SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null && pwd )"
SRC=$(realpath $SCRIPT_DIR/..)

set -e # fail on first error

if [[ $# -lt 1 ]]; then
  echo "Usage: $0 version"
  exit 1
fi

VERSION=$1

check_if_dirty(){
  pushd . > /dev/null # silence output
  cd $SRC
  if [[ ! -z $(git status --short) ]]; then
    echo "ERROR: Git checkout in $(pwd) is dirty!"
	echo "Please, commit your local changes as this script needs to switch to the master branch"
	exit 2
  fi
  popd > /dev/null # silence output
}

tag() {
	cd $SRC
	pushd . > /dev/null # silence output
	git checkout master --quiet
	git pull --quiet
	git fetch --tags --force --quiet
	CURRENT_HASH=$(git show-ref origin/master -s)
	echo "Tagging $NAME @ $CURRENT_HASH with tag: $VERSION"
	git tag -a $VERSION -m "Tagging version $VERSION"
	git push origin refs/tags/$VERSION:refs/tags/$VERSION
	popd > /dev/null # silence output
}

check_if_dirty
tag
