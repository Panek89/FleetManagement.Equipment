resource "azurerm_mssql_database" "mssql_db_fleetmanagement_equipment" {
  name      = "mssql-dbequipment-${var.fm_suffix}-${var.env_suffix}"
  server_id = data.azurerm_mssql_server.shared_mssql_server.id
  collation = "SQL_Latin1_General_CP1_CI_AS"
  sku_name  = "Free"
}

resource "azurerm_mssql_firewall_rule" "allow_my_ip" {
  count = var.is_local ? 1 : 0

  name             = "AllowLocalDevIP-Equipment"
  server_id        = data.azurerm_mssql_server.shared_mssql_server.id
  start_ip_address = trimspace(data.http.my_public_ip.response_body)
  end_ip_address   = trimspace(data.http.my_public_ip.response_body)
}
