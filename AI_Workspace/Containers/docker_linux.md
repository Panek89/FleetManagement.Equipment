# Docker Container Instructions for Linux/macOS (Bash)

## Prerequisites
- Docker must be installed and the daemon must be running.

## 1. Runtime Health Check
```sh
if ! command -v docker &> /dev/null; then
    echo "Docker CLI is not installed."
    exit 1
fi

if ! docker info &> /dev/null; then
    echo "Docker engine is not running. Please start it and retry."
    exit 1
fi
```

## 2. Start SQL Server Container
```sh
if [ "$(docker ps -a --filter "name=^/sql2025$" --format "{{.Names}}")" ]; then
    docker start sql2025
else
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyPassword123!!" -e "MSSQL_PID=StandardDeveloper" -p 1433:1433 --name sql2025 --hostname sql2025 -d mcr.microsoft.com/mssql/server:2025-latest
fi

echo "Waiting for SQL Server to be ready..."
for i in {1..20}; do
    if docker exec sql2025 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "MyPassword123!!" -C -Q "SELECT 1" &> /dev/null; then
        echo "SQL Server is ready!"
        break
    fi
    echo "Waiting for SQL Server engine ($i/20)..."
    sleep 3
done
```

## 3. Build & Run Equipment Microservice
```sh
SERVICE_NAME="fleet-management-equipment-local"

# Build Image
docker build -t fleet-management-equipment .

# Run Container
if [ "$(docker ps -a --filter "name=^/$SERVICE_NAME$" --format "{{.Names}}")" ]; then
    docker stop $SERVICE_NAME
    docker rm $SERVICE_NAME
fi

docker run -d --name $SERVICE_NAME -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ConnectionStrings__SqlDatabase="Server=host.docker.internal,1433;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;" \
  fleet-management-equipment
```

## 4. Verification
Check the API documentation at: `http://localhost:8080/scalar/v1`
```sh
docker ps --filter "name=sql2025"
docker ps --filter "name=fleet-management-equipment-local"
```
