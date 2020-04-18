@echo off
cd /D src/Identity/
dotnet clean
dotnet restore
dotnet build
dotnet run --configuration Debug
pause
