SHELL := /bin/bash

SLN_NAME := OnlineClothes
MIGRATE_EXECUTED_CONTEXT := -p ./$(SLN_NAME).Persistence/ -s ./$(SLN_NAME).Api/
COMPOSE_DEV_CONTEXT := docker-compose.dev.override.yaml

.PHONY: docker

test:
	ifeq (foo, $(filter foo,$(MAKECMDGOALS)))
		@echo $foo
	endif

migrate:
	dotnet ef migrations add $(name) $(MIGRATE_EXECUTED_CONTEXT)

migrate-list:
	dotnet ef migrations list $(MIGRATE_EXECUTED_CONTEXT)

migrate-apply:
	dotnet ef database update $(MIGRATE_EXECUTED_CONTEXT)

migrate-remove:
	dotnet ef migrations remove $(MIGRATE_EXECUTED_CONTEXT)

build-dev:
	docker-compose -f docker-compose.yaml -f $(COMPOSE_DEV_CONTEXT) build

compose-dev-up:
	cd ./scripts && bash compose-dev.sh up

compose-dev-down:
	cd ./scripts && bash compose-dev.sh down