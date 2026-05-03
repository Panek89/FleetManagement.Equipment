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

### 1. Docker Compose (Recommended for local dev)

The easiest way to start both the SQL Server and the API is using Docker Compose.

```bash
docker-compose up -d
```

This will:
1. Start a **SQL Server 2025** container.
2. Wait for the database to be healthy.
3. Build and start the **Equipment API**.
4. Configure the API to connect to the SQL container automatically.

To stop the services:
```bash
docker-compose down
```

### 2. Podman Compose (Linux Alternative)

If you are using **Podman**, you can use `podman-compose` in a similar way:

```bash
podman-compose up -d
```

To stop the services:
```bash
podman-compose down
```

> **Note:** `podman-compose` might not be installed by default. You can install it via your package manager or pip:
> - **Fedora/RHEL:** `sudo dnf install podman-compose`
> - **Ubuntu/Debian:** `sudo apt install podman-compose`
> - **Python/PIP:** `pip install podman-compose`

> **Note:** Depending on your setup, you might need to ensure the Podman socket is active if you are using the standard `docker-compose` binary with Podman.

### 3. Manual Run

If you prefer to run containers manually, ensure you have a SQL Server instance running first. 

> **Quick Start:** If you don't have SQL Server ready, follow the [Start SQL Server Container](database.md#start-sql-server-container) guide.

To run the application container, you need to provide the SQL connection string via an environment variable (`ConnectionStrings__SqlDatabase`). 

> **Host Resolution Note:** 
> - **Docker (Windows/Mac):** Use `host.docker.internal`.
> - **Docker (Linux):** Use the container's IP/Name or host IP.
> - **Podman (Linux):** Use `host.containers.internal` or `10.0.2.2`.

#### PowerShell (Windows - Docker)
```powershell
docker run -p 8080:8080 `
  -e ASPNETCORE_ENVIRONMENT=Development `
  -e ConnectionStrings__SqlDatabase="Server=host.docker.internal;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;" `
  fleet-management-equipment
```

#### Bash (Linux / macOS - Docker)
```bash
docker run -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ConnectionStrings__SqlDatabase="Server=host.docker.internal;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;" \
  fleet-management-equipment
```

#### Bash (Linux - Podman Alternative)
```bash
podman run -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ConnectionStrings__SqlDatabase="Server=host.containers.internal;Database=FleetManagement.Equipment;User Id=sa;Password=MyPassword123!!;Encrypt=True;TrustServerCertificate=True;" \
  fleet-management-equipment
```

## Accessing the API

Once running, you can access the application and its documentation at:

- **API Documentation (Scalar):** [http://localhost:8080/scalar/v1](http://localhost:8080/scalar/v1)
- **OpenAPI Spec (JSON):** [http://localhost:8080/openapi/v1.json](http://localhost:8080/openapi/v1.json)

---

## 🤖 Agent-Driven Setup (Recommended)

If you are using an AI Agent (like Gemini CLI), you can automate the entire setup process. The agent will detect your OS, check if Docker/Podman is running, start the SQL Server, and build/run the microservice.

### How to use
Tell the agent:
> *"Run the containerization setup instructions"*

The agent will follow the instructions located in `AI_Workspace/CONTAINER_AGENT_INSTRUCTIONS.md`.

### Prerequisites for Agent Setup
| Platform | Tool | Prerequisites |
| :--- | :--- | :--- |
| **Windows** | **Docker** | Docker Desktop installed, `pwsh` (PowerShell 7+) |
| **Windows** | **Podman** | Podman Desktop/CLI installed, `pwsh` (PowerShell 7+) |
| **Linux/macOS** | **Docker** | Docker Engine installed and running |
| **Linux/macOS** | **Podman** | Podman installed and initialized |

---

## ⚠️ Important Notes & Troubleshooting

### 🔑 Connection Strings & Passwords
The default setup uses `MyPassword123!!` and `Server=host.docker.internal` (or `host.containers.internal`). 
- **Warning:** If you manually change the password in the SQL container but don't update the `ConnectionStrings__SqlDatabase` environment variable when running the API, the application **will fail** to connect.
- **Agent Consistency:** The Agent scripts use consistent values. If you want to change them, you must update the files in `AI_Workspace/Containers/`.

### 🚫 Port Conflicts
The setup uses:
- **Port 1433**: For SQL Server.
- **Port 8080**: For the Equipment API.
If these ports are already in use by other local services, the containers will fail to start.

### 🐘 Docker/Podman Engine Status
- **Windows:** Ensure Docker Desktop or Podman Machine is actually started. The Agent scripts attempt to start them, but if they are incorrectly installed, manual intervention is required.
- **WSL2:** If using Docker on Windows, ensure your WSL2 integration is enabled for your distribution.

### 🧬 Database Readiness
SQL Server takes ~20-30 seconds to fully initialize. The Agent-driven scripts include a "Wait for Ready" loop. If you run manually, wait for the SQL logs to show *"Recovery is complete"* before starting the API.
