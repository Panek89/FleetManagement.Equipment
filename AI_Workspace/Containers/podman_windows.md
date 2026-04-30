# Podman Container Instructions for Windows (PowerShell)

## Prerequisites
- Verify `pwsh` (PowerShell 7+) is available:
  ```powershell
  pwsh -NoProfile -Command "$PSVersionTable.PSVersion"
  ```
- Podman must be installed and initialized.

## 1. Runtime Health Check
```powershell
if (-not (Get-Command podman -ErrorAction SilentlyContinue)) {
    throw "Podman CLI is not installed."
}

podman info *> $null
if ($LASTEXITCODE -ne 0) {
    Write-Host "Podman machine is not running. Attempting to start..."
    podman machine start
    if ($LASTEXITCODE -ne 0) { throw "Failed to start Podman machine. Please check manually." }
}
```

## 2. Start SQL Server Container
```powershell
if (podman ps -a --filter "name=^/sql2025$" --format "{{.Names}}") {
    podman start sql2025
} else {
    podman run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyPassword123!!" -e "MSSQL_PID=StandardDeveloper" -p 1433:1433 --name sql2025 --hostname sql2025 -d mcr.microsoft.com/mssql/server:2025-latest
}

Write-Host "Waiting for SQL Server to be ready..."
$maxRetries = 20
for ($i = 1; $i -le $maxRetries; $i++) {
    podman exec sql2025 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "MyPassword123!!" -C -Q "SELECT 1" *> $null
    if ($LASTEXITCODE -eq 0) {
        Write-Host "SQL Server is ready!"
        break
    }
    Write-Host "Waiting for SQL Server engine ($i/$maxRetries)..."
    Start-Sleep -Seconds 3
}
```

## 3. Build & Run Equipment Microservice
```powershell
$ServiceName = "fleet-management-equipment-local"

# Build Image
podman build -t fleet-management-equipment .

# Run Container
if (podman ps -a --filter "name=^/$ServiceName$" --format "{{.Names}}") {
    podman stop $ServiceName; podman rm $ServiceName
}

podman run -d --name $ServiceName -p 8080:8080 `
  -e ASPNETCORE_ENVIRONMENT=Development `
  -e ConnectionStrings__SqlDatabase="Server=host.containers.internal,1433;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;" `
  fleet-management-equipment

## 4. Verification
Check the API documentation at: `http://localhost:8080/scalar/v1`
```powershell
podman ps --filter "name=sql2025"
podman ps --filter "name=fleet-management-equipment-local"
```

```
