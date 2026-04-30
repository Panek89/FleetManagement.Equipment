# Podman Container Instructions for Linux/macOS (Bash)

## Prerequisites
- Podman must be installed and initialized.

## 1. Runtime Health Check
```sh
if ! command -v podman &> /dev/null; then
    echo "Podman CLI is not installed."
    exit 1
fi

if ! podman info &> /dev/null; then
    echo "Podman machine is not running. Attempting to start..."
    podman machine start
    if [ $? -ne 0 ]; then echo "Failed to start Podman. Check manually."; exit 1; fi
fi
```

## 2. Start SQL Server Container
```sh
if [ "$(podman ps -a --filter "name=^/sql2025$" --format "{{.Names}}")" ]; then
    podman start sql2025
else
    podman run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyPassword123!!" -e "MSSQL_PID=StandardDeveloper" -p 1433:1433 --name sql2025 --hostname sql2025 -d mcr.microsoft.com/mssql/server:2025-latest
fi

echo "Waiting for SQL Server to be ready..."
for i in {1..20}; do
    if podman exec sql2025 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "MyPassword123!!" -C -Q "SELECT 1" &> /dev/null; then
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
podman build -t fleet-management-equipment .

# Run Container
if [ "$(podman ps -a --filter "name=^/$SERVICE_NAME$" --format "{{.Names}}")" ]; then
    podman stop $SERVICE_NAME
    podman rm $SERVICE_NAME
fi

podman run -d --name $SERVICE_NAME -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ConnectionStrings__SqlDatabase="Server=host.containers.internal,1433;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;" \
  fleet-management-equipment
```

## 4. Verification
Check the API documentation at: `http://localhost:8080/scalar/v1`
```sh
podman ps --filter "name=sql2025"
podman ps --filter "name=fleet-management-equipment-local"
```
