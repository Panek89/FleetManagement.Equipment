variable "shared_resource_group_name" {
  type        = string
  description = "Name of the shared resource group"
}

variable "location" {
  type        = string
  description = "Azure region for resources"
  default     = "westeurope"
}

variable "env_suffix" {
  type        = string
  description = "Environment suffix (e.g. dev, prod)"
}

variable "fm_suffix" {
  type        = string
  description = "Suffix for FleetManagement project"
}

variable "mssql_server_name" {
  type        = string
  description = "Name of the existing MSSQL Server"
}

variable "app_configuration_name" {
  type        = string
  description = "Name of the existing App Configuration"
}

variable "is_local" {
  type        = bool
  default     = false
  description = "Set to true if running from local machine to allow IP in firewall"
}

variable "docker_image" {
  type        = string
  description = "Full Docker image name (e.g. username/repository:tag)"
  default     = "panekdev/fleetmanagement-equipment:latest"
}
