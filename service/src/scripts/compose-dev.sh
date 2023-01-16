#! /bin/bash

# move to src dir
cd ../

if [ ! -z $1 ] && [ $1 = "down" ]
then
    docker-compose -f docker-compose.yaml -f docker-compose.dev.override.yaml -p oop_src down --remove-orphans
else
    docker-compose -f docker-compose.yaml -f docker-compose.dev.override.yaml -p oop_src up -d --remove-orphans
fi
