@echo off
set tests_unit="Identity.Tests"
set tests_end_to_end="Identity.EndToEnd"

cd /D src

echo Run unit tests

cd /D %tests_unit%
dotnet test
cd /D ../

echo Run end to end tests

cd /D %tests_end_to_end%
dotnet test

cd /D ../../
pause
