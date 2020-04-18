@echo off
docker-compose -f "identity_stack\docker-compose.yml" start
cd /D src/Identity/
dotnet run --configuration Debug
