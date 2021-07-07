resource "aws_iam_policy" "developer_codecommit_role_policy" {
  name        = "DeveloperCodeCommitRolePolicy"
  path        = "/"
  description = "Developer Code Commit Role Policy"

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Effect = "Allow",
        Action = [
          "codecommit:BatchGet*",
          "codecommit:Create*",
          "codecommit:DeleteBranch",
          "codecommit:Get*",
          "codecommit:List*",
          "codecommit:Describe*",
          "codecommit:Put*",
          "codecommit:Post*",
          "codecommit:Merge*",
          "codecommit:Test*",
          "codecommit:Update*",
          "codecommit:GitPull",
          "codecommit:GitPush"
        ],
        "Resource" : [
          "arn:aws:codecommit:ap-southeast-2:${var.production_account_id}:*"
        ]
      },
      {
        "Effect" : "Allow",
        "Action" : "codecommit:ListRepositories",
        "Resource" : "*"
      }
    ]
  })
}

resource "aws_iam_role" "developer_codecommit_role" {
  name = "DeveloperCodeCommitRole"

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Effect = "Allow"
        Action = "sts:AssumeRole",
        Principal = {
          AWS = "arn:aws:iam::${var.development_account_id}:root"
        }
      }
    ]
  })

  managed_policy_arns = [aws_iam_policy.developer_codecommit_role_policy.arn]
}
