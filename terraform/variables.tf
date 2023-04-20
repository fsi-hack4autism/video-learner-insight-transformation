variable "resource_group_name" {
  type        = string
  description = "Azure resource group name"
}

variable "function_app_name" {
  type        = string
  description = "The name of the function app"
}

variable "cosmosdb_account_name" {
  type        = string
  description = "The name of the Cosmos DB account."
}

variable "cosmosdb_sql_database_name" {
  type        = string
  description = "The name of the Cosmos DB SQL database."
}

variable "videoindexer_account_name" {
  type        = string
  description = "The name of the Video Indexer account."
}
  