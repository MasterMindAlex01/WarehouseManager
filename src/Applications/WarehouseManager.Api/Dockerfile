#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Applications/WarehouseManager.Api/WarehouseManager.Api.csproj", "src/Applications/WarehouseManager.Api/"]
COPY ["src/Infrastructure/WarehouseManager.Infrastructure/WarehouseManager.Infrastructure.csproj", "src/Infrastructure/WarehouseManager.Infrastructure/"]
COPY ["src/Core/WarehouseManager.Application/WarehouseManager.Application.csproj", "src/Core/WarehouseManager.Application/"]
COPY ["src/Core/WarehouseManager.Domain/WarehouseManager.Domain.csproj", "src/Core/WarehouseManager.Domain/"]
COPY ["src/Core/WarehouseManager.Shared/WarehouseManager.Shared.csproj", "src/Core/WarehouseManager.Shared/"]
COPY ["src/Migrators/WarehouseManager.Migrators.MSSQL/WarehouseManager.Migrators.MSSQL.csproj", "src/Migrators/WarehouseManager.Migrators.MSSQL/"]
RUN dotnet restore "./src/Applications/WarehouseManager.Api/WarehouseManager.Api.csproj"
COPY . .
WORKDIR "/src/src/Applications/WarehouseManager.Api"
RUN dotnet build "./WarehouseManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WarehouseManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WarehouseManager.Api.dll"]