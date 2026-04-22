[← Back to README](../README.md)

# Local Development & Database Setup

This guide covers how to run the application locally, configure the database connection, and manage Entity Framework Core migrations.

## Table of Contents

- [Running Locally](#running-locally)
- [Database Migrations](#database-migrations)

---

## Running Locally

To run and test the application locally you need a running SQL Server instance. The easiest way is to use a container.

### Start SQL Server Container

**Docker:**
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyPassword123!!" -e "MSSQL_PID=StandardDeveloper" -p 1433:1433 --name sql2025 --hostname sql2025 -d mcr.microsoft.com/mssql/server:2025-latest
```

**Podman:**
```bash
podman run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyPassword123!!" -e "MSSQL_PID=StandardDeveloper" -p 1433:1433 --name sql2025 --hostname sql2025 -d mcr.microsoft.com/mssql/server:2025-latest
```

### Configure Connection String

Once the container is running, provide the connection string via .NET user secrets (recommended) or `appsettings.Development.json`.

**Using user secrets (recommended — keeps secrets out of tracked files):**
```bash
dotnet user-secrets set "ConnectionStrings:SqlDatabase" \
  "Server=localhost,1433;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;" \
  --project FleetManagement.Equipment.API
```

**Alternatively**, set it directly in `FleetManagement.Equipment.API/appsettings.Development.json` (do not commit credentials):
```json
{
  "ConnectionStrings": {
    "SqlDatabase": "Server=localhost,1433;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;"
  }
}
```

### Run the Application

```bash
dotnet run --project FleetManagement.Equipment.API/FleetManagement.Equipment.API.csproj
```

The application will automatically apply any pending migrations on startup.

---

## Database Migrations

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) must be installed.
- The [EF Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) must be available. Install them globally with:
	```bash
	dotnet tool install --global dotnet-ef
	```
- Ensure all project dependencies are restored:
	```bash
	dotnet restore
	```

### Creating a Migration

To create a new EF Core migration, run from the solution root:

```bash
dotnet ef migrations add <MigrationName> \
  -p FleetManagement.Equipment.Infrastructure/FleetManagement.Equipment.Infrastructure.csproj \
  -s FleetManagement.Equipment.API/FleetManagement.Equipment.API.csproj
```

**Why this command?**  
The `DbContext` lives in the Infrastructure project while the entry point (DI, configuration) lives in the API project. Specifying both ensures:
- Migrations are written to the correct project (`-p` = Infrastructure).
- Runtime configuration (connection string, services) is loaded from the correct startup project (`-s` = API).

**Command breakdown:**
| Flag | Purpose |
|------|---------|
| `migrations add <Name>` | Creates a new migration with the given name |
| `-p ...Infrastructure.csproj` | Project containing the `DbContext` and migration history |
| `-s ...API.csproj` | Startup project used to load DI and `appsettings` |

### Applying Migrations

To apply pending migrations to the database, run from the solution root:

```bash
dotnet ef database update \
  -p FleetManagement.Equipment.Infrastructure/FleetManagement.Equipment.Infrastructure.csproj \
  -s FleetManagement.Equipment.API/FleetManagement.Equipment.API.csproj
```

This creates or updates tables, keys, and indexes to match the current model. After running it, the `__EFMigrationsHistory` table in the database should contain an entry for each applied migration.

> **Note:** The application also auto-applies pending migrations on startup, so manual `database update` is mainly useful during development or in CI pipelines where you want to separate migration from application start.
