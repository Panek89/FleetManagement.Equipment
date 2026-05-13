resource "azurerm_storage_account" "st_equipment_functions" {
  name                     = lower(replace("staccfnequipmentfm${var.env_suffix}", "/[-_]/", ""))
  resource_group_name      = azurerm_resource_group.equipment_functions_rg.name
  location                 = azurerm_resource_group.equipment_functions_rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  blob_properties {
    versioning_enabled = true
  }

  tags = {
    environment = var.env_suffix
    project     = "FleetManagement"
    component   = "EquipmentFunctions"
  }
}

resource "azurerm_storage_container" "functions_container" {
  name                  = "functions-data"
  storage_account_id    = azurerm_storage_account.st_equipment_functions.id
  container_access_type = "private"
}
