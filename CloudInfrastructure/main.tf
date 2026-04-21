terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=4.69.0"
    }
  }
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "resource_group" {
  name     = "rg-${var.resource-suffix}"
  location = var.location-region
}

resource "azurerm_app_configuration" "app_configuration" {
  name                = "appconf-${var.resource-suffix}"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = var.location-region
  sku                 = "free"
}
