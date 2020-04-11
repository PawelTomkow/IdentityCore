@echo off
echo Clean docker images
docker-compose -f "identity_stack\docker-compose.yml" down
echo Install docker images
docker-compose -f "identity_stack\docker-compose.yml" up --build --no-start