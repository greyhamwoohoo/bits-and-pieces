# AWS Cross Account Code Commit Access
Allow access to CodeCommit repositories in one AWS Account by assuming a role. The user assuming the role is in another AWS Account. 

## Production and Development
Two 'accounts' are described here: Production (which includes the CodeCommit repository and Role) and Development (which includes the user that will access the repository)

### To set up Production
In the Production folder, it is assumed the resources will be created in ap-southeast-2 using a local .aws/credentials and .aws/config profile of 'prod-administrator':

```bash
terraform init
terraform plan
terraform apply
```

### To set up Development
TODO: 

## References
| Description | Link |
| ----------- | ---- |
| Configure Cross-Account Repository Access using Roles | https://docs.aws.amazon.com/codecommit/latest/userguide/cross-account.html | 
