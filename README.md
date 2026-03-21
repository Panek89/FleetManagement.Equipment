# FleetManagement.Equipment
Microservice for Fleet Management system


## Table of Contents

- [Database Migrations](#database-migrations)

## Database Migrations

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) must be installed.
- The [EF Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) must be available. You can install them globally with:
	```
	dotnet tool install --global dotnet-ef
	```
- Ensure all project dependencies are restored:
	```
	dotnet restore
	```

### Local Development Configuration

For local development, make sure to configure the connection string in `FleetManagement.Equipment.API/appsettings.Development.json` under the `ConnectionStrings:SqlDatabase` section. This file should contain the correct settings for your local SQL Server instance.

### Creating a Migration

To create a new Entity Framework Core migration, use the following command:

```
dotnet ef migrations add InitialCreate -p .\FleetManagement.Equipment.Infrastructure\FleetManagement.Equipment.Infrastructure.csproj -s .\FleetManagement.Equipment.API\FleetManagement.Equipment.API.csproj
```

#### Why This Command?

This command is tailored for a solution where the Entity Framework Core DbContext is located in a different project (Infrastructure) than the startup project (API). This is a common pattern in clean architecture and DDD-based solutions, where separation of concerns is maintained between the API, domain, and infrastructure layers.

By specifying both the migration project (`-p`) and the startup project (`-s`), you ensure that:
- Migrations are added to the correct project (Infrastructure), which contains the `AppDbContext` and migration history.
- The application is started from the correct entry point (API), so all configuration (like dependency injection, appsettings, etc.) is loaded as it would be in production or development.

#### Command Breakdown

- `dotnet ef migrations add InitialCreate` — Adds a new migration named `InitialCreate`.
- `-p .\FleetManagement.Equipment.Infrastructure\FleetManagement.Equipment.Infrastructure.csproj` — Specifies the project where the migrations and DbContext are located (Infrastructure).
- `-s .\FleetManagement.Equipment.API\FleetManagement.Equipment.API.csproj` — Specifies the startup project to use for configuration and dependency injection (API).

This approach ensures migrations are generated with the correct context and configuration, avoiding issues with missing dependencies or misconfigured services.

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) must be installed.
- The [EF Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) must be available. You can install them globally with:
	```
	dotnet tool install --global dotnet-ef
	```
- Ensure all project dependencies are restored:
	```
	dotnet restore
	```

### Local Development Configuration

For local development, make sure to configure the connection string in `FleetManagement.Equipment.API/appsettings.Development.json` under the `ConnectionStrings:SqlDatabase` section. This file should contain the correct settings for your local SQL Server instance.
