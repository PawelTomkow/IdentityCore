@echo off
git stash
git checkout master
git pull
docker-compose -f "identity_stack\docker-compose.yml" build identity.app