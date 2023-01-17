SHELL := /bin/bash

SLN_NAME := OnlineClothes
MIGRATE_EXECUTED_CONTEXT := -p ./$(SLN_NAME).Persistence/ -s ./$(SLN_NAME).Api/
COMPOSE_DEV_CONTEXT := docker-compose.dev.override.yaml

.PHONY: test, build

migrate:
	dotnet ef migrations add $(name) $(MIGRATE_EXECUTED_CONTEXT)

migrate-list:
	dotnet ef migrations list $(MIGRATE_EXECUTED_CONTEXT)

migrate-apply:
	dotnet ef database update $(MIGRATE_EXECUTED_CONTEXT)

migrate-remove:
	dotnet ef migrations remove $(MIGRATE_EXECUTED_CONTEXT)

test:
	dotnet test --blame --verbosity quiet
	
build:
	docker-compose -f docker-compose.yaml -f docker-compose.dev.override.yaml build

publish:
	docker-compose -f docker-compose.yaml build
	docker push solcr/online-clothes-api

dev-up:
	cd ./scripts && bash compose-dev.sh up

dev-down:
	cd ./scripts && bash compose-dev.sh down