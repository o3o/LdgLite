#!/bin/sh +v
NEW_VER=$@
git checkout -b release-${NEW_VER}
