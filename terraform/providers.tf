terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">= 3.0.0, < 4.0.0"
    }
    azapi = {
      source  = "azure/azapi"
      version = ">= 1.0.0, < 2.0.0"
    }
  }
}

provider "azurerm" {
  features {}
}

provider "azapi" {}