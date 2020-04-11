@echo off
git stash
git checkout master
git pull
docker-compose -f "docker_compose\docker-compose.yml" up --build --no-start