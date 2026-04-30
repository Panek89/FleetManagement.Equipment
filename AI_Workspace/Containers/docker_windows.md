# Docker Container Instructions for Windows (PowerShell)

## Prerequisites
- Verify `pwsh` (PowerShell 7+) is available:
  ```powershell
  pwsh -NoProfile -Command "$PSVersionTable.PSVersion"
  ```
- Docker Desktop must be installed and running.

## 1. Runtime Health Check
```powershell
if (-not (Get-Command docker -ErrorAction SilentlyContinue)) {
    throw "Docker CLI is not installed."
}

docker info *> $null
if ($LASTEXITCODE -ne 0) {
    # Attempt to start Docker Desktop if not running
    $DockerDesktopPath = @(
        "$Env:ProgramFiles\Docker\Docker\Docker Desktop.exe",
        "$Env:ProgramFiles(x86)\Docker\Docker\Docker Desktop.exe"
    ) | Where-Object { $_ -and (Test-Path $_) } | Select-Object -First 1

    if (-not $DockerDesktopPath) {
        throw "Docker engine is not running and Docker Desktop executable was not found. Please start it manually."
    }
    
    Start-Process $DockerDesktopPath
    Write-Host "Starting Docker Desktop... Checking status..."
    
    $maxRetries = 20
    $retryCount = 0
    while ($retryCount -lt $maxRetries) {
        docker info *> $null
        if ($LASTEXITCODE -eq 0) {
            Write-Host "Docker is ready!"
            break
        }
        $retryCount++
        Write-Host "Waiting for Docker engine... ($($retryCount)/$($maxRetries))"
        Start-Sleep -Seconds 5
    }

    if ($retryCount -eq $maxRetries) {
        throw "Docker failed to start within a reasonable time. Please verify manually."
    }
}
```

## 2. Start SQL Server Container
```powershell
if (docker ps -a --filter "name=^/sql2025$" --format "{{.Names}}") {
    docker start sql2025
} else {
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyPassword123!!" -e "MSSQL_PID=StandardDeveloper" -p 1433:1433 --name sql2025 --hostname sql2025 -d mcr.microsoft.com/mssql/server:2025-latest
}

Write-Host "Waiting for SQL Server to be ready..."
$maxRetries = 20
for ($i = 1; $i -le $maxRetries; $i++) {
    docker exec sql2025 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "MyPassword123!!" -C -Q "SELECT 1" *> $null
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
docker build -t fleet-management-equipment .

# Run Container
if (docker ps -a --filter "name=^/$ServiceName$" --format "{{.Names}}") {
    docker stop $ServiceName; docker rm $ServiceName
}

docker run -d --name $ServiceName -p 8080:8080 `
  -e ASPNETCORE_ENVIRONMENT=Development `
  -e ConnectionStrings__SqlDatabase="Server=host.docker.internal,1433;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;" `
  fleet-management-equipment

## 4. Verification
Check the API documentation at: `http://localhost:8080/scalar/v1`
```powershell
docker ps --filter "name=sql2025"
docker ps --filter "name=fleet-management-equipment-local"
```

```
