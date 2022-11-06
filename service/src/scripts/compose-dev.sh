#! /bin/bash

# move to src dir
cd ../

if [ $1 = "down" ]
then
    docker-compose -f docker-compose.dev.yaml -p oop-infra down
else
    docker-compose -f docker-compose.dev.yaml -p oop-infra up -d
fi
