data "azurerm_app_configuration_key" "mssql_password" {
  configuration_store_id = data.azurerm_app_configuration.shared_appconf.id
  key                    = "Mssql:AdminPassword"
  label                  = var.env_suffix
}

locals {
  sql_conn_string = "Server=tcp:${data.azurerm_mssql_server.shared_mssql_server.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.mssql_db_fleetmanagement_equipment.name};Persist Security Info=False;User ID=${data.azurerm_mssql_server.shared_mssql_server.administrator_login};Password=${data.azurerm_app_configuration_key.mssql_password.value};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

resource "azurerm_app_configuration_key" "mssql_connection_string" {
  configuration_store_id = data.azurerm_app_configuration.shared_appconf.id
  key                    = "Mssql:DbEquipmentConnectionString"
  type                   = "kv"
  label                  = var.env_suffix
  value                  = local.sql_conn_string
}
