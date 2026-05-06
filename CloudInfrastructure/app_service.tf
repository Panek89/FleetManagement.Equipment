resource "azurerm_service_plan" "asp_equipment" {
  name                = "asp-equipment-${var.fm_suffix}-${var.env_suffix}"
  resource_group_name = data.azurerm_resource_group.shared_rg.name
  location            = data.azurerm_resource_group.shared_rg.location
  os_type             = "Linux"
  sku_name            = "F1"
}

resource "azurerm_linux_web_app" "app_equipment" {
  name                = "app-equipment-${var.fm_suffix}-${var.env_suffix}"
  resource_group_name = data.azurerm_resource_group.shared_rg.name
  location            = data.azurerm_resource_group.shared_rg.location
  service_plan_id     = azurerm_service_plan.asp_equipment.id

  site_config {
    always_on = false
    application_stack {
      docker_image_name   = var.docker_image
      docker_registry_url = "https://index.docker.io"
    }
  }

  app_settings = {
    "Azure__AppConfig"      = data.azurerm_app_configuration.shared_appconf.endpoint
    "Azure__AppConfigLabel" = var.env_suffix
    "WEBSITES_PORT"         = "8080"
  }

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_role_assignment" "app_equipment_appconf_reader" {
  scope                = data.azurerm_app_configuration.shared_appconf.id
  role_definition_name = "App Configuration Data Reader"
  principal_id         = azurerm_linux_web_app.app_equipment.identity[0].principal_id
}

resource "azurerm_mssql_firewall_rule" "allow_azure_services" {
  name             = "AllowAzureServices-Equipment"
  server_id        = data.azurerm_mssql_server.shared_mssql_server.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}
