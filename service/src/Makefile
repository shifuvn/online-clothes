SHELL := /bin/bash
SLN_NAME = OnlineClothes
CONTEXT_EXECUTE_MIGRATE = -p ./$(SLN_NAME).Persistence/ -s ./$(SLN_NAME).Api/


test:
	ifeq (foo, $(filter foo,$(MAKECMDGOALS)))
		@echo $foo
	endif

migrate:
	dotnet ef migrations add $(name) $(CONTEXT_EXECUTE_MIGRATE)

migrate-list:
	dotnet ef migrations list $(CONTEXT_EXECUTE_MIGRATE)

migrate-apply:
	dotnet ef database update $(CONTEXT_EXECUTE_MIGRATE)

migrate-remove:
	dotnet ef migrations remove $(CONTEXT_EXECUTE_MIGRATE)

compose-up:
	docker-compose -f docker-compose.dev.yaml -p oop_src up -d --remove-orphans

compose-down:
	docker-compose -f docker-compose.dev.yaml -p oop_src down
