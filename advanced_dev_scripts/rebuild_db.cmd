@echo off
docker-compose -f "..\identity_stack\docker-compose.yml" up sql.database --build --no-start