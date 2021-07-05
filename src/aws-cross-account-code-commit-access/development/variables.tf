# AWS Account ID: The Role, RolePolicy and CodeCommit Repositories are assumed to exist in this account.
variable "production_account_id" {
  type    = string
  default = "AWS Account ID (Production)"
}

# AWS Account ID: The User Group and User Group Policy will be created in this account. 
variable "development_account_id" {
  type    = string
  default = "AWS Account ID (Development)"
}
