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
  # AWS Profile to configure AWS (Developer Account)
  profile = "dev-administrator"
}
