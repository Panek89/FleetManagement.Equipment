resource "azurerm_storage_account" "st_equipment_functions" {
  name                     = lower(replace("stequipfn${var.fm_suffix}${var.env_suffix}", "/[-_]/", ""))
  resource_group_name      = data.azurerm_resource_group.shared_rg.name
  location                 = data.azurerm_resource_group.shared_rg.location
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
