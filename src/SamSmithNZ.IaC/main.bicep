// Main Bicep file for SamSmithNZ infrastructure
targetScope = 'resourceGroup'

@description('Location for all resources')
param location string = 'eastus'

@description('Azure AD tenant ID for Key Vault')
param tenantId string

@description('Key Vault access policies')
param keyVaultAccessPolicies array = [
  {
    tenantId: '40244aae-8aee-4af5-b221-b480db24698b'
    objectId: '992c5b97-b5ab-40df-bad7-63ded66f24da'
    permissions: {
      keys: [
        'Get'
        'List'
        'Update'
        'Create'
        'Import'
        'Delete'
        'Recover'
        'Backup'
        'Restore'
      ]
      secrets: [
        'Get'
        'List'
        'Set'
        'Delete'
        'Recover'
        'Backup'
        'Restore'
      ]
      certificates: [
        'Get'
        'List'
        'Update'
        'Create'
        'Import'
        'Delete'
        'Recover'
        'Backup'
        'Restore'
        'ManageContacts'
        'ManageIssuers'
        'GetIssuers'
        'ListIssuers'
        'SetIssuers'
        'DeleteIssuers'
      ]
    }
  }
  {
    tenantId: '40244aae-8aee-4af5-b221-b480db24698b'
    objectId: '3521d452-978e-49ee-8ae0-55ef55cc2386'
    permissions: {
      keys: [
        'Get'
        'List'
      ]
      secrets: []
      certificates: []
    }
  }
  {
    tenantId: '40244aae-8aee-4af5-b221-b480db24698b'
    objectId: 'a9ffa7b9-d3c3-4053-a529-d55422c68102'
    permissions: {
      keys: []
      secrets: [
        'Get'
        'List'
      ]
      certificates: []
    }
  }
  {
    tenantId: '40244aae-8aee-4af5-b221-b480db24698b'
    objectId: '05608eb4-dd81-4d75-97db-4227fede5816'
    permissions: {
      keys: [
        'Get'
        'List'
        'Update'
        'Create'
        'Import'
        'Delete'
        'Recover'
        'Backup'
        'Restore'
        'Decrypt'
        'Encrypt'
        'UnwrapKey'
        'WrapKey'
        'Verify'
        'Sign'
        'Purge'
      ]
      secrets: []
      certificates: []
    }
  }
]

// Deploy Application Insights
module monitoring './modules/monitoring.bicep' = {
  name: 'monitoring-deployment'
  params: {
    appInsightsName: 'ssnzapplicationinsights'
    location: location
  }
}

// Deploy App Services
module appServices './modules/appServices.bicep' = {
  name: 'appservices-deployment'
  params: {
    appServicePlanName: 'ssnzwinserviceplan'
    location: 'East US'
    webAppName: 'ssnz-prod-eu-web'
    serviceAppName: 'ssnz-prod-eu-service'
    mandmServiceAppName: 'mandm-prod-eu-service'
    customDomainVerificationId: '94F391F7B468D7216F02F51D8356390A89B6F3949B2E9888D070424D1169AF7C'
  }
}

// Deploy Key Vault
module keyVault './modules/keyVault.bicep' = {
  name: 'keyvault-deployment'
  params: {
    keyVaultName: 'ssnzkeyvault'
    location: location
    tenantId: tenantId
    accessPolicies: keyVaultAccessPolicies
  }
}

// Deploy Storage Account
module storage './modules/storage.bicep' = {
  name: 'storage-deployment'
  params: {
    storageAccountName: 'ssnzdbserverlogstorage'
    location: location
  }
}

output appInsightsInstrumentationKey string = monitoring.outputs.appInsightsInstrumentationKey
output webAppName string = appServices.outputs.webAppName
output serviceAppName string = appServices.outputs.serviceAppName
output mandmServiceAppName string = appServices.outputs.mandmServiceAppName
output keyVaultUri string = keyVault.outputs.keyVaultUri
output storageAccountName string = storage.outputs.storageAccountName
