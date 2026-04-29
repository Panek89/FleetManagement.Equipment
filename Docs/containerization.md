[← Back to README](../README.md)

# Containerization

The application can be containerized using the provided `Dockerfile` located in the root of the `FleetManagement.Equipment` directory. While these instructions use **Docker**, you can also use **Podman** as an alternative (common on Linux).

## Build the image

From the `FleetManagement.Equipment` directory, run:

```bash
# Using Docker
docker build -t fleet-management-equipment .

# Using Podman (Alternative)
podman build -t fleet-management-equipment .
```

## Run the container

Before running the application container, ensure you have a SQL Server instance running. 

> **Quick Start:** If you don't have SQL Server ready, follow the [Start SQL Server Container](database.md#start-sql-server-container) guide.

To run the application container, you need to provide the SQL connection string via an environment variable (`ConnectionStrings__SqlDatabase`). 

> **Host Resolution Note:** 
> - **Docker (Windows/Mac):** Use `host.docker.internal`.
> - **Docker (Linux):** Use the container's IP/Name or host IP.
> - **Podman (Linux):** Use `host.containers.internal` or `10.0.2.2`.

### PowerShell (Windows - Docker)
```powershell
docker run -p 8080:8080 `
  -e ASPNETCORE_ENVIRONMENT=Development `
  -e ConnectionStrings__SqlDatabase="Server=host.docker.internal;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;" `
  fleet-management-equipment
```

### Bash (Linux / macOS - Docker)
```bash
docker run -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ConnectionStrings__SqlDatabase="Server=host.docker.internal;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;" \
  fleet-management-equipment
```

### Bash (Linux - Podman Alternative)
```bash
podman run -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ConnectionStrings__SqlDatabase="Server=host.containers.internal;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;" \
  fleet-management-equipment
```

## Accessing the API

Once running, you can access the application and its documentation at:

- **API Base URL:** [http://localhost:8080](http://localhost:8080)
- **API Documentation (Scalar):** [http://localhost:8080/scalar/v1](http://localhost:8080/scalar/v1)
- **OpenAPI Spec (JSON):** [http://localhost:8080/openapi/v1.json](http://localhost:8080/openapi/v1.json)
