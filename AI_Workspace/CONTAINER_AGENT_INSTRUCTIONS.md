# AI Agent Container Instructions (Router)

Use this document ONLY for **containerized** local setup.
If user wants normal local development (without containers), use `AI_Workspace/DEV_INSTRUCTIONS.md`.

## 1. Determine Environment & Tool
1. **Detect OS**: Check the session context. (Current OS detected as: win32)
2. **Ask User**: "Which container tool do you want to use? (Docker / Podman)"

## 2. Load Specific Instructions
Based on the OS and the chosen tool, load and follow **ONLY ONE** of the files below:

### Windows (detected as win32)
- For Docker: Read `AI_Workspace/Containers/docker_windows.md`
- For Podman: Read `AI_Workspace/Containers/podman_windows.md`

### Linux / macOS
- For Docker: Read `AI_Workspace/Containers/docker_linux.md`
- For Podman: Read `AI_Workspace/Containers/podman_linux.md`

**CRITICAL**: Do NOT load multiple instruction files. Load only the one that matches the current OS and user preference to minimize context usage.
