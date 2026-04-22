variable "resource-suffix" {
  type        = string
  description = "Resource group name"
  default     = "fleetmanagement"
}

variable "location-region" {
  type        = string
  description = "Location region"
  default     = "West Europe"
}

variable "is_local" {
  type        = bool
  default     = true
  description = "Ustaw na false, jeśli uruchamiasz kod w Pipeline"
}
