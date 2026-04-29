[← Back to README](../../README.md)

# Docker Hub Pipeline Integration (Azure DevOps)

This guide outlines the best practices for building and pushing Docker images to Docker Hub using Azure DevOps pipelines.

## 1. Authorization: Service Connection
To avoid exposing credentials in the pipeline YAML, use an Azure DevOps Service Connection.

1.  **Generate a PAT:** 
    - Log in to [Docker Hub](https://hub.docker.com/).
    - Go to **Account Settings > Security**.
    - Click **New Access Token**. 
    - Note the token (Personal Access Token - PAT). This is safer than your primary password.
2.  **Create Service Connection in Azure DevOps:**
    - Go to **Project Settings > Service connections**.
    - Click **New service connection** -> **Docker Registry**.
    - Select **Docker Hub**.
    - **Docker ID:** Your Docker Hub username.
    - **Docker Password:** The PAT generated in the previous step.
    - **Service connection name:** `dockerhub-connection` (use this name in your YAML).
    - Grant access permission to all pipelines.

## 2. Configuration: Variable Groups
Store your image names in the existing Variable Groups or define them in the YAML.

- `docker_repository`: `yourusername/fleetmanagement-equipment`
- `docker_image_tag`: `$(Build.BuildId)`

## 3. Implementation: YAML Task
Add the following task to `fleetmanagement_equipment_pipeline.yaml`.

```yaml
variables:
  docker_repository: 'yourusername/fleetmanagement-equipment'
  docker_connection: 'dockerhub-connection'

steps:
# ... other steps ...

- task: Docker@2
  displayName: "Build and Push to Docker Hub"
  inputs:
    containerRegistry: '$(docker_connection)'
    repository: '$(docker_repository)'
    command: 'buildAndPush'
    Dockerfile: '$(Build.SourcesDirectory)/Dockerfile'
    tags: |
      latest
      $(Build.BuildId)
```

## Benefits of this approach
- **Security:** Credentials are encrypted and handled automatically by the Service Connection.
- **Traceability:** Using `$(Build.BuildId)` as a tag links images directly to pipeline runs.
- **Simplicity:** The `Docker@2` task handles login, build, and push in a single optimized block.
