#! /bin/bash

# working directory will be setuped in action file

# run docker-compose build
docker-compose -f docker-compose.dev.yaml -p oop-infra build