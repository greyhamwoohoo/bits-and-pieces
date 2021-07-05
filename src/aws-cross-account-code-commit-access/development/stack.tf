resource "aws_iam_group" "cross_account_developers_user_group" {
  name = "CrossAccountDevelopers"
  path = "/"
}

resource "aws_iam_policy" "cross_account_developers_user_group_policy" {
  name        = "CrossAccountDevelopersPolicy"
  path        = "/"
  description = "Cross Account Developers Policy"

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Effect = "Allow",
        Action = "sts:AssumeRole",
        Resource = "arn:aws:iam::${var.production_account_id}:role/DeveloperCodeCommitRole"
      },
    ]
  })
}

resource "aws_iam_group_policy_attachment" "cross_account_developers_user_group_policy_attachment" {
  group      = aws_iam_group.cross_account_developers_user_group.name
  policy_arn = aws_iam_policy.cross_account_developers_user_group_policy.arn
}
