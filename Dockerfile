#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 18766

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Identity/Identity.csproj", "Identity/"]
COPY ["Identity.Core/Identity.Core.csproj", "Identity.Core/"]
COPY ["Identity.Persistence/Identity.Persistence.csproj", "Identity.Persistence/"]
COPY ["Identity.Application/Identity.Application.csproj", "Identity.Application/"]
RUN dotnet restore "Identity/Identity.csproj"
COPY . .
WORKDIR "/src/Identity"
RUN dotnet build "Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.dll"]