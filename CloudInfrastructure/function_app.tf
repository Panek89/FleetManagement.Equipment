resource "azurerm_linux_function_app" "func_equipment" {
  name                = "func-equipment-${var.fm_suffix}-${var.env_suffix}"
  resource_group_name = data.azurerm_resource_group.shared_rg.name
  location            = data.azurerm_resource_group.shared_rg.location

  storage_account_name       = azurerm_storage_account.st_equipment_functions.name
  storage_account_access_key = azurerm_storage_account.st_equipment_functions.primary_access_key
  service_plan_id            = azurerm_service_plan.asp_equipment.id

  site_config {
    application_stack {
      dotnet_version              = "10.0"
      use_dotnet_isolated_runtime = true
    }
  }

  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME"     = "dotnet-isolated"
    "EQUIPMENT_DB_HEALTHCHECK_URL" = "https://${azurerm_linux_web_app.app_equipment.default_hostname}/api/healthcheck/db"
  }

  identity {
    type = "SystemAssigned"
  }
}
