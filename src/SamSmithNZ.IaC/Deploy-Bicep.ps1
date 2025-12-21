#Requires -Version 7.0

<#
.SYNOPSIS
    Deploy Azure infrastructure using Bicep templates

.DESCRIPTION
    This script deploys the SamSmithNZ infrastructure to Azure using Bicep.
    It supports both main infrastructure and database deployments.

.PARAMETER ResourceGroupName
    Name of the Azure resource group

.PARAMETER ResourceGroupLocation
    Azure region for the resource group

.PARAMETER DeploymentType
    Type of deployment: 'Main' for main infrastructure, 'Database' for SQL database, or 'All' for both

.PARAMETER ValidateOnly
    If specified, only validates the template without deploying

.PARAMETER SqlAdminPassword
    SQL Server administrator password (required for database deployment)

.EXAMPLE
    .\Deploy-Bicep.ps1 -ResourceGroupName "SamSmithNZ" -ResourceGroupLocation "eastus" -DeploymentType "Main"

.EXAMPLE
    .\Deploy-Bicep.ps1 -ResourceGroupName "SamSmithNZ" -ResourceGroupLocation "eastus" -DeploymentType "Database" -SqlAdminPassword (ConvertTo-SecureString "P@ssw0rd!" -AsPlainText -Force)

.EXAMPLE
    .\Deploy-Bicep.ps1 -ResourceGroupName "SamSmithNZ" -ResourceGroupLocation "eastus" -DeploymentType "All" -SqlAdminPassword (ConvertTo-SecureString "P@ssw0rd!" -AsPlainText -Force)
#>

Param(
    [Parameter(Mandatory=$true)]
    [string] $ResourceGroupName,
    
    [Parameter(Mandatory=$true)]
    [string] $ResourceGroupLocation,
    
    [Parameter(Mandatory=$false)]
    [ValidateSet('Main', 'Database', 'All')]
    [string] $DeploymentType = 'Main',
    
    [Parameter(Mandatory=$false)]
    [switch] $ValidateOnly,
    
    [Parameter(Mandatory=$false)]
    [SecureString] $SqlAdminPassword
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version 3

# Get script directory
$ScriptDirectory = Split-Path -Parent $PSCommandPath

# Check if Azure CLI is installed
if (-not (Get-Command az -ErrorAction SilentlyContinue)) {
    Write-Error "Azure CLI is not installed. Please install it from https://docs.microsoft.com/cli/azure/install-azure-cli"
    exit 1
}

# Check if logged in to Azure
$account = az account show 2>$null | ConvertFrom-Json
if (-not $account) {
    Write-Host "Not logged in to Azure. Please run 'az login' first." -ForegroundColor Red
    exit 1
}

Write-Host "Using Azure subscription: $($account.name) ($($account.id))" -ForegroundColor Green

# Create resource group if it doesn't exist
$rg = az group show --name $ResourceGroupName 2>$null | ConvertFrom-Json
if (-not $rg) {
    Write-Host "Creating resource group '$ResourceGroupName' in '$ResourceGroupLocation'..." -ForegroundColor Yellow
    az group create --name $ResourceGroupName --location $ResourceGroupLocation
    Write-Host "Resource group created successfully." -ForegroundColor Green
} else {
    Write-Host "Resource group '$ResourceGroupName' already exists." -ForegroundColor Green
}

# Function to deploy Bicep template
function Deploy-BicepTemplate {
    param(
        [string]$TemplateFile,
        [string]$ParametersFile,
        [string]$DeploymentName,
        [hashtable]$AdditionalParameters = @{}
    )
    
    $templatePath = Join-Path $ScriptDirectory $TemplateFile
    $parametersPath = Join-Path $ScriptDirectory $ParametersFile
    
    if (-not (Test-Path $templatePath)) {
        Write-Error "Template file not found: $templatePath"
        return $false
    }
    
    if (-not (Test-Path $parametersPath)) {
        Write-Error "Parameters file not found: $parametersPath"
        return $false
    }
    
    Write-Host "`nDeploying $DeploymentName..." -ForegroundColor Cyan
    
    $deployArgs = @(
        'deployment', 'group', 'create'
        '--resource-group', $ResourceGroupName
        '--name', $DeploymentName
        '--template-file', $templatePath
        '--parameters', $parametersPath
    )
    
    # Add additional parameters
    foreach ($key in $AdditionalParameters.Keys) {
        $deployArgs += "--parameters"
        $deployArgs += "$key=$($AdditionalParameters[$key])"
    }
    
    if ($ValidateOnly) {
        Write-Host "Validating template..." -ForegroundColor Yellow
        $deployArgs[2] = 'validate'
    }
    
    try {
        $result = & az @deployArgs 2>&1
        
        if ($LASTEXITCODE -eq 0) {
            if ($ValidateOnly) {
                Write-Host "Validation successful for $DeploymentName!" -ForegroundColor Green
            } else {
                Write-Host "Deployment successful for $DeploymentName!" -ForegroundColor Green
            }
            return $true
        } else {
            Write-Host "Error during deployment of $DeploymentName" -ForegroundColor Red
            Write-Host $result -ForegroundColor Red
            return $false
        }
    } catch {
        Write-Host "Exception during deployment of $DeploymentName" -ForegroundColor Red
        Write-Host $_.Exception.Message -ForegroundColor Red
        return $false
    }
}

# Deploy based on deployment type
$success = $true

if ($DeploymentType -eq 'Main' -or $DeploymentType -eq 'All') {
    Write-Host "`n=== Deploying Main Infrastructure ===" -ForegroundColor Magenta
    $deploymentName = "main-infrastructure-$(Get-Date -Format 'yyyyMMdd-HHmmss')"
    $result = Deploy-BicepTemplate -TemplateFile "main.bicep" -ParametersFile "main.bicepparam" -DeploymentName $deploymentName
    
    if (-not $result) {
        $success = $false
        if ($DeploymentType -eq 'All') {
            Write-Host "Main infrastructure deployment failed. Skipping database deployment." -ForegroundColor Red
        }
    }
}

if (($DeploymentType -eq 'Database' -or $DeploymentType -eq 'All') -and $success) {
    Write-Host "`n=== Deploying Database ===" -ForegroundColor Magenta
    
    if (-not $SqlAdminPassword) {
        Write-Error "SQL Admin Password is required for database deployment. Use -SqlAdminPassword parameter."
        $success = $false
    } else {
        $deploymentName = "database-$(Get-Date -Format 'yyyyMMdd-HHmmss')"
        
        # Convert SecureString to plain text for Azure CLI
        $BSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($SqlAdminPassword)
        $plainPassword = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)
        [System.Runtime.InteropServices.Marshal]::ZeroFreeBSTR($BSTR)
        
        $additionalParams = @{
            'administratorLoginPassword' = $plainPassword
        }
        
        $result = Deploy-BicepTemplate -TemplateFile "modules/database.bicep" -ParametersFile "database.bicepparam" -DeploymentName $deploymentName -AdditionalParameters $additionalParams
        
        if (-not $result) {
            $success = $false
        }
    }
}

# Summary
Write-Host "`n=== Deployment Summary ===" -ForegroundColor Magenta
if ($success) {
    if ($ValidateOnly) {
        Write-Host "All validations completed successfully!" -ForegroundColor Green
    } else {
        Write-Host "All deployments completed successfully!" -ForegroundColor Green
    }
    exit 0
} else {
    Write-Host "One or more deployments failed. Please check the errors above." -ForegroundColor Red
    exit 1
}
