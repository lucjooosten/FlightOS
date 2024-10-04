# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FlightOS.Api/FlightOS.Api.csproj", "FlightOS.Api/"]
COPY ["FlightOS.Application/FlightOS.Application.csproj", "FlightOS.Application/"]
COPY ["FlightOS.Domain/FlightOS.Domain.csproj", "FlightOS.Domain/"]
COPY ["FlightOS.Infrastructure/FlightOS.Infrastructure.csproj", "FlightOS.Infrastructure/"]
RUN dotnet restore "./FlightOS.Api/FlightOS.Api.csproj"
COPY . .
WORKDIR "/src/FlightOS.Api"
RUN dotnet build "./FlightOS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FlightOS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlightOS.Api.dll"]