terraform {
  required_providers {
    aws = {
      source = "hashicorp/aws"
      version = "3.48.0"
    }
  }
}

provider "aws" {
  region = "ap-southeast-2"
  # AWS Profile to configure AWS (Production Account)
  profile = "prod-administrator"
}
