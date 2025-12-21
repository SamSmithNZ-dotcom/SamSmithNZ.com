// Key Vault module
@description('Name of the Key Vault')
param keyVaultName string = 'ssnzkeyvault'

@description('Location for the Key Vault')
param location string = 'eastus'

@description('Azure AD tenant ID')
param tenantId string

@description('Access policies for the Key Vault')
param accessPolicies array = []

resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' = {
  name: keyVaultName
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: tenantId
    accessPolicies: accessPolicies
    enabledForDeployment: false
    enabledForDiskEncryption: false
    enabledForTemplateDeployment: false
  }
}

output keyVaultId string = keyVault.id
output keyVaultName string = keyVault.name
output keyVaultUri string = keyVault.properties.vaultUri
