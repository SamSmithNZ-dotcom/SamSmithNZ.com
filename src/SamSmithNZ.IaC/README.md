# SamSmithNZ Infrastructure as Code

This directory contains the Azure infrastructure definitions using **Bicep**, Microsoft's domain-specific language for deploying Azure resources.

## Overview

The infrastructure is organized into modular Bicep files for better maintainability and reusability:

### Main Files
- **`main.bicep`** - Main orchestration file that deploys the core infrastructure
- **`main.bicepparam`** - Parameters file for main infrastructure deployment
- **`database.bicepparam`** - Parameters file for database deployment
- **`Deploy-Bicep.ps1`** - PowerShell deployment script

### Modules
- **`modules/monitoring.bicep`** - Application Insights for monitoring
- **`modules/appServices.bicep`** - App Service Plan and Web/Service Apps
- **`modules/keyVault.bicep`** - Azure Key Vault for secrets management
- **`modules/storage.bicep`** - Storage Account with blob containers
- **`modules/database.bicep`** - SQL Server and Database with firewall rules

## Resources Deployed

### Main Infrastructure (`main.bicep`)
- **Application Insights**: `ssnzapplicationinsights`
- **App Service Plan**: `ssnzwinserviceplan` (Basic B1 tier)
- **Web Apps**:
  - `ssnz-prod-eu-web` (main website with SSL for samsmithnz.com and www.samsmithnz.com)
  - `ssnz-prod-eu-service` (service API)
  - `mandm-prod-eu-service` (M&M service API)
- **Key Vault**: `ssnzkeyvault`
- **Storage Account**: `ssnzdbserverlogstorage` with containers for logs, Let's Encrypt, and Azure WebJobs

### Database Infrastructure (`modules/database.bicep`)
- **SQL Server**: `ssnzdbserver` (version 12.0)
- **SQL Database**: `SSNZDB` (Basic tier, 2GB max size)
- **Firewall Rules**: Multiple IP ranges for secure access

## Prerequisites

1. **Azure CLI** (version 2.20.0 or later)
   ```bash
   az --version
   ```
   Install from: https://docs.microsoft.com/cli/azure/install-azure-cli

2. **Bicep CLI** (bundled with Azure CLI 2.20.0+)
   ```bash
   az bicep version
   ```

3. **PowerShell** (version 7.0 or later) - for using the deployment script
   ```bash
   pwsh --version
   ```

4. **Azure Subscription** with appropriate permissions to create resources

## Deployment

### Login to Azure
```bash
az login
```

Set your subscription (if you have multiple):
```bash
az account set --subscription "<subscription-id-or-name>"
```

### Option 1: Using PowerShell Script (Recommended)

#### Deploy Main Infrastructure Only
```powershell
.\Deploy-Bicep.ps1 -ResourceGroupName "SamSmithNZ" -ResourceGroupLocation "eastus" -DeploymentType "Main"
```

#### Deploy Database Only
```powershell
$sqlPassword = ConvertTo-SecureString "YourStrongPassword123!" -AsPlainText -Force
.\Deploy-Bicep.ps1 -ResourceGroupName "SamSmithNZ" -ResourceGroupLocation "eastus" -DeploymentType "Database" -SqlAdminPassword $sqlPassword
```

#### Deploy Everything
```powershell
$sqlPassword = ConvertTo-SecureString "YourStrongPassword123!" -AsPlainText -Force
.\Deploy-Bicep.ps1 -ResourceGroupName "SamSmithNZ" -ResourceGroupLocation "eastus" -DeploymentType "All" -SqlAdminPassword $sqlPassword
```

#### Validate Templates Without Deploying
```powershell
.\Deploy-Bicep.ps1 -ResourceGroupName "SamSmithNZ" -ResourceGroupLocation "eastus" -DeploymentType "Main" -ValidateOnly
```

### Option 2: Using Azure CLI Directly

#### Create Resource Group
```bash
az group create --name SamSmithNZ --location eastus
```

#### Deploy Main Infrastructure
```bash
az deployment group create \
  --resource-group SamSmithNZ \
  --name main-deployment \
  --template-file main.bicep \
  --parameters main.bicepparam
```

#### Deploy Database
```bash
az deployment group create \
  --resource-group SamSmithNZ \
  --name database-deployment \
  --template-file modules/database.bicep \
  --parameters database.bicepparam \
  --parameters administratorLoginPassword='YourStrongPassword123!'
```

## Validation

Before deploying, you can validate the Bicep templates:

```bash
# Validate main infrastructure
az deployment group validate \
  --resource-group SamSmithNZ \
  --template-file main.bicep \
  --parameters main.bicepparam

# Validate database
az deployment group validate \
  --resource-group SamSmithNZ \
  --template-file modules/database.bicep \
  --parameters database.bicepparam \
  --parameters administratorLoginPassword='YourStrongPassword123!'
```

## Build Bicep to ARM JSON (Optional)

If you need to generate ARM JSON templates from Bicep:

```bash
az bicep build --file main.bicep
az bicep build --file modules/database.bicep
```

This will create `main.json` and `database.json` files.

## Customization

### Modifying Parameters

Edit the `.bicepparam` files to change default values:
- **`main.bicepparam`** - Modify tenant ID, location, etc.
- **`database.bicepparam`** - Modify SQL server name, database name, firewall rules, etc.

### Adding Firewall Rules

Edit `database.bicepparam` and add new rules to the `firewallRules` array:

```bicep
{
  name: 'MyNewRule'
  startIpAddress: '192.168.1.1'
  endIpAddress: '192.168.1.255'
}
```

### Modifying Key Vault Access Policies

Edit the `keyVaultAccessPolicies` parameter in `main.bicep` to add or modify access policies.

## Outputs

After successful deployment, the following outputs are available:

From **main.bicep**:
- Application Insights Instrumentation Key
- Web App Names
- Key Vault URI
- Storage Account Name

From **database.bicep**:
- SQL Server Name
- SQL Database Name

View outputs:
```bash
az deployment group show \
  --resource-group SamSmithNZ \
  --name main-deployment \
  --query properties.outputs
```

## Migration from ARM Templates

This Bicep infrastructure replaces the legacy ARM template deployment:
- ✅ `azuredeploy.json` → `main.bicep` + modules
- ✅ `azuredeployDB.json` → `modules/database.bicep`
- ✅ `Deploy-AzureResourceGroup.ps1` → `Deploy-Bicep.ps1`
- ✅ `SamSmithNZ.IaC.deployproj` → No longer needed (removed)

## Troubleshooting

### Common Issues

1. **Bicep not found**: Upgrade Azure CLI to version 2.20.0 or later
   ```bash
   az upgrade
   ```

2. **Authentication errors**: Ensure you're logged in and have the right subscription selected
   ```bash
   az login
   az account show
   ```

3. **Resource already exists**: If resources exist, deployment will update them in place (where supported)

4. **Firewall rule conflicts**: If deploying to existing SQL Server, ensure firewall rule names don't conflict

## Resources

- [Bicep Documentation](https://learn.microsoft.com/azure/azure-resource-manager/bicep/)
- [Bicep Best Practices](https://learn.microsoft.com/azure/azure-resource-manager/bicep/best-practices)
- [Azure CLI Reference](https://learn.microsoft.com/cli/azure/)
- [Migrating from ARM to Bicep](https://learn.microsoft.com/azure/azure-resource-manager/bicep/decompile)

## Support

For issues or questions, please open an issue in the repository.
