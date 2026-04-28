terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=4.69.0"
    }
  }

  backend "azurerm" {
  }
}

provider "azurerm" {
  features {}
}

data "azurerm_client_config" "current" {}

data "http" "my_public_ip" {
  url = "https://ifconfig.me/ip"
}

data "azurerm_resource_group" "shared_rg" {
  name = var.shared_resource_group_name
}

data "azurerm_mssql_server" "shared_mssql_server" {
  name                = var.mssql_server_name
  resource_group_name = data.azurerm_resource_group.shared_rg.name
}

data "azurerm_app_configuration" "shared_appconf" {
  name                = var.app_configuration_name
  resource_group_name = data.azurerm_resource_group.shared_rg.name
}
