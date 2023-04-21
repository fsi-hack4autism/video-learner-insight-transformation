resource "azurerm_cosmosdb_account" "this" {
  name                = var.cosmosdb_account_name
  resource_group_name = data.azurerm_resource_group.rg.name
  location            = data.azurerm_resource_group.rg.location
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"
  
  consistency_policy {
    consistency_level = "Session"
  }
  geo_location {
    location          = data.azurerm_resource_group.rg.location
    failover_priority = 0
  }
}

resource "azurerm_cosmosdb_sql_database" "this" {
  name                = var.cosmosdb_sql_database_name
  resource_group_name = data.azurerm_resource_group.rg.name
  account_name        = azurerm_cosmosdb_account.this.name
  throughput          = 400
}

resource "azurerm_cosmosdb_sql_container" "users" {
  name                = "users"
  resource_group_name = data.azurerm_resource_group.rg.name
  account_name        = azurerm_cosmosdb_account.this.name
  database_name       = azurerm_cosmosdb_sql_database.this.name
  partition_key_path  = "/id"
  throughput          = 400
}

resource "azurerm_cosmosdb_sql_container" "videos" {
  name                = "videos"
  resource_group_name = data.azurerm_resource_group.rg.name
  account_name        = azurerm_cosmosdb_account.this.name
  database_name       = azurerm_cosmosdb_sql_database.this.name
  partition_key_path  = "/id"
  throughput          = 400
}