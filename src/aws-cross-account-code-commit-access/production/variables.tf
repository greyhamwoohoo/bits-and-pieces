# The Role and Policy will be created in the Production account.
variable "production_account_id" {
  type    = string
  default = "AWS Account ID (Production)"
}

# The ID of the Development Account is required so Production can set up a Trust Relationship with it. 
variable "development_account_id" {
  type    = string
  default = "AWS Account ID (Development)"
}
