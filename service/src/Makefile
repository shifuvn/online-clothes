SHELL := /bin/bash

SLN_NAME := OnlineClothes
MIGRATE_EXECUTED_CONTEXT := -p ./$(SLN_NAME).Persistence/ -s ./$(SLN_NAME).Api/
COMPOSE_CONTEXT := docker-compose.dev.yaml

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

build:
	docker-compose -f $(COMPOSE_CONTEXT) build
	
docker:
	cd ./scripts && bash compose-dev.sh $(compose)
