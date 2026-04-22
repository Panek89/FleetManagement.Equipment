resource "azurerm_mssql_server" "mssql_server" {
  name                = "mssql-server-${var.resource-suffix}"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = var.location-region
  version             = "12.0"
  administrator_login = "sqladmin"

  administrator_login_password = random_password.sql_admin_password.result

  depends_on = [azurerm_app_configuration.app_configuration]
}

resource "azurerm_mssql_database" "mssql_db_fleetmanagement_equipment" {
  name      = "mssql-dbequipment-${var.resource-suffix}"
  server_id = azurerm_mssql_server.mssql_server.id
  collation = "SQL_Latin1_General_CP1_CI_AS"
  sku_name  = "Free"
}

resource "azurerm_mssql_firewall_rule" "allow_my_ip" {
  count = var.is_local ? 1 : 0

  name             = "AllowLocalDevIP"
  server_id        = azurerm_mssql_server.mssql_server.id
  start_ip_address = trimspace(data.http.my_public_ip.response_body)
  end_ip_address   = trimspace(data.http.my_public_ip.response_body)
}
