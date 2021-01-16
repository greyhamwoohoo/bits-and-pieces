# Exploratory script to retrieve all test results from all builds in a given pipeline
#
# Author: greyhamwoohoo
#
# $env:AZDOPS_PAT="yourPat"
# $env:AZDOPS_ACCOUNT="greyhamwoohoo"
# $env:AZDOPS_PROJECT_NAME="Public-Automation-Examples"
# $env:AZDOPS_PIPELINE_NAME="interface-interceptor-core-pr"
# $env:AZDOPS_AUTHORIZATION_BASE64 = [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(":$($env:AZDOPS_PAT)")

# Pre-req:
# Install-Module VsTeam 
Set-StrictMode -Version Latest
$ErrorActionPreference="Stop"
$VerbosePreference="Continue"

# 1. Get Pipeline / Build Definition
$definition = Get-VSTeamBuildDefinition -ProjectName $env:AZDOPS_PROJECT_NAME | ?{ $_.Name -eq $env:AZDOPS_PIPELINE_NAME }
if($definition -eq $null) {
    throw "ERROR: The pipeline '$($env:AZDOPS_PIPELINE_NAME)' does not exist in the Project '$($env:AZDOPS_PROJECT_NAME)'"
}

# 2. Get all of the builds for that definition
 $builds = @(Get-VSTeamBuild -ProjectName $env:AZDOPS_PROJECT_NAME -Definitions $definition.Id | Sort-Object -Property StartTime -Descending)

# 3. For Each Build
($builds).ForEach{

    $build = $_

    Write-Host $build.InternalObject.sourceVersion
    Write-Host $build.InternalObject.sourceBranch    

    $startTime = $build.StartTime.AddDays(-1).ToString("yyyy-MM-dd")
    $endTime = $build.StartTime.AddDays(1).ToString("yyyy-MM-dd")

    # 3.1 Get the Test Runs in this build (my understanding is: this is a result for each of the Vstest tasks or PublishTestTasks)
    # NOTE: minLast.../maxLast... are mandatory which seems a bit redundant given I have the buildId directly. Alas. 
    $response = Invoke-RestMethod -Method GET -Uri "https://dev.azure.com/$($env:AZDOPS_ACCOUNT)/$($env:AZDOPS_PROJECT_NAME)/_apis/test/runs?minLastUpdatedDate=$($startTime)&maxLastUpdatedDate=$($endTime)&buildIds=$($build.Id)&api-version=6.0" -Headers @{"Authorization"="Basic $($env:AZDOPS_AUTHORIZATION_BASE64)"}

    ($response.value).ForEach{

        $run = $_

        Write-Host $run.name
        Write-Host $run.pipelineReference.stageReference
        Write-Host $run.pipelineReference.phaseReference
        Write-Host $run.pipelineReference.jobReference

        Write-Host $run.runStatistics

        # NOTE: We only need to do this if we want the individual results for each and every test
        $response = Invoke-RestMethod -Method GET -Uri "https://dev.azure.com/$($env:AZDOPS_ACCOUNT)/$($env:AZDOPS_PROJECT_NAME)/_apis/test/runs/$($run.Id)/results" -Headers @{"Authorization"="Basic $($env:AZDOPS_AUTHORIZATION_BASE64)"}
    }
}
