resource "azurerm_app_configuration" "app_configuration" {
  name                = "appconf-${var.resource-suffix}"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = var.location-region
  sku                 = "free"
}

resource "azurerm_role_assignment" "appconf_data_owner" {
  scope                = azurerm_app_configuration.app_configuration.id
  role_definition_name = "App Configuration Data Owner"
  principal_id         = data.azurerm_client_config.current.object_id
}

resource "azurerm_app_configuration_key" "mssql_password_key" {
  configuration_store_id = azurerm_app_configuration.app_configuration.id
  key                    = "Mssql:AdminPassword"
  type                   = "kv"
  label                  = "production"
  value                  = random_password.sql_admin_password.result

  depends_on = [azurerm_mssql_server.mssql_server, azurerm_role_assignment.appconf_data_owner]
}

locals {
  sql_conn_string = "Server=tcp:${azurerm_mssql_server.mssql_server.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.mssql_db_fleetmanagement.name};Persist Security Info=False;User ID=${azurerm_mssql_server.mssql_server.administrator_login};Password=${random_password.sql_admin_password.result};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

resource "azurerm_app_configuration_key" "mssql_connection_string" {
  configuration_store_id = azurerm_app_configuration.app_configuration.id
  key                    = "Mssql:ConnectionString"
  type                   = "kv"
  label                  = "production"
  value                  = local.sql_conn_string

  depends_on = [azurerm_mssql_server.mssql_server, azurerm_mssql_database.mssql_db_fleetmanagement, azurerm_role_assignment.appconf_data_owner]
}
