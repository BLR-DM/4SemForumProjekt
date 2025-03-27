# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Adjust the paths to copy the .csproj files
COPY ["ContentSafetyService.Api/ContentSafetyService.Api.csproj", "ContentSafetyService.Api/"]
COPY ["ContentSafetyService.Application/ContentSafetyService.Application.csproj", "ContentSafetyService.Application/"]
COPY ["ContentSafetyService.Domain/ContentSafetyService.Domain.csproj", "ContentSafetyService.Domain/"]
COPY ["ContentSafetyService.Infrastructure/ContentSafetyService.Infrastructure.csproj", "ContentSafetyService.Infrastructure/"]


RUN dotnet restore "ContentSafetyService.Api/ContentSafetyService.Api.csproj"

# Copy the entire source code
COPY .. .

WORKDIR "/src/ContentSafetyService.Api"
RUN dotnet build "ContentSafetyService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ContentSafetyService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContentSafetyService.Api.dll"]

# To build the image run the following command
# docker build -t contentsafetyservice -f ContentSafetyService.Api/Dockerfile .