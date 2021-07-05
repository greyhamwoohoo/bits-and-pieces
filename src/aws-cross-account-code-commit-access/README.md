# AWS Cross Account Code Commit Access
Allow access to CodeCommit repositories in one AWS Account from another account by assuming a role. The user assuming the role is in another AWS Account. 

## Production and Development
Two 'accounts' are described here: 

1. Production (which includes the CodeCommit repository and Role)
2. Development (which includes the user that will access the repository)

### To set up Production
In the Production folder, it is assumed the resources will be created in ap-southeast-2 using a local .aws/credentials and .aws/config profile of 'prod-administrator':

```bash
cd production
terraform init
terraform plan
terraform apply
```

### To set up Development
In the Production folder, it is assumed the resources will be created in ap-southeast-2 using a local .aws/credentials and .aws/config profile of 'dev-administrator':

```
cd development
terraform init
terraform plan
terraform apply
```

The above will create a IAM user group called 'CrossAccountDevelopers' and 'CrossAccountDevelopersPolicy': add any user into that group and they will be able to access the CodeCommit repositories in Production (assuming you are using sts or something like git-remote-codecommit: see reference below on how to set it up)

## References
| Description | Link |
| ----------- | ---- |
| Configure Cross-Account Repository Access using Roles | https://docs.aws.amazon.com/codecommit/latest/userguide/cross-account.html | 
