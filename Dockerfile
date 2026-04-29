# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy all csproj files and restore
COPY ["FleetManagement.Equipment.API/FleetManagement.Equipment.API.csproj", "FleetManagement.Equipment.API/"]
COPY ["FleetManagement.Equipment.Application/FleetManagement.Equipment.Application.csproj", "FleetManagement.Equipment.Application/"]
COPY ["FleetManagement.Equipment.Infrastructure/FleetManagement.Equipment.Infrastructure.csproj", "FleetManagement.Equipment.Infrastructure/"]
COPY ["FleetManagement.Equipment.Domain/FleetManagement.Equipment.Domain.csproj", "FleetManagement.Equipment.Domain/"]
COPY ["FleetManagement.Equipment.Shared/FleetManagement.Equipment.Shared.csproj", "FleetManagement.Equipment.Shared/"]

RUN dotnet restore "FleetManagement.Equipment.API/FleetManagement.Equipment.API.csproj"

# Copy the rest of the source code
COPY . .

# Build and publish
WORKDIR "/src/FleetManagement.Equipment.API"
RUN dotnet publish "FleetManagement.Equipment.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Final image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FleetManagement.Equipment.API.dll"]
