#!/bin/sh +v
NEW_VER=$@
git commit -a -m "Incrementato versione a ${NEW_VER}"
