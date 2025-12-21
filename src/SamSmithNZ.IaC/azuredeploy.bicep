// AUTO-GENERATED FILE - FOR REFERENCE ONLY
// This file was automatically decompiled from the original ARM template.
// The production-ready modular Bicep files are:
//   - main.bicep (orchestration)
//   - modules/monitoring.bicep
//   - modules/appServices.bicep
//   - modules/keyVault.bicep
//   - modules/storage.bicep
// 
// Use the modular files for deployments, not this auto-generated file.

param sites_ssnz_prod_eu_web_name string = 'ssnz-prod-eu-web'
param sites_ssnz_prod_eu_service_name string = 'ssnz-prod-eu-service'
param sites_mandm_prod_eu_service_name string = 'mandm-prod-eu-service'
param serverfarms_ssnzwinserviceplan_name string = 'ssnzwinserviceplan'
param components_ssnzapplicationinsights_name string = 'ssnzapplicationinsights'
param certificates_www_samsmithnz_com_ssnz_prod_eu_web_name string = 'www.samsmithnz.com-ssnz-prod-eu-web'
param vaults_ssnzkeyvault_name string = 'ssnzkeyvault'
param storageAccounts_ssnzdbserverlogstorage_name string = 'ssnzdbserverlogstorage'

resource components_ssnzapplicationinsights_name_resource 'microsoft.insights/components@2020-02-02' = {
  name: components_ssnzapplicationinsights_name
  location: 'eastus'
  kind: 'web'
  properties: {
    Application_Type: 'web'
    Flow_Type: 'Brownfield'
    Request_Source: 'VSIX8.13.10627.1'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

resource serverfarms_ssnzwinserviceplan_name_resource 'Microsoft.Web/serverfarms@2021-01-15' = {
  name: serverfarms_ssnzwinserviceplan_name
  location: 'East US'
  sku: {
    name: 'B1'
    tier: 'Basic'
    size: 'B1'
    family: 'B'
    capacity: 1
  }
  kind: 'app'
  properties: {
    perSiteScaling: false
    elasticScaleEnabled: false
    maximumElasticWorkerCount: 0
    isSpot: false
    reserved: false
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
  }
}

resource sites_mandm_prod_eu_service_name_resource 'Microsoft.Web/sites@2021-01-15' = {
  name: sites_mandm_prod_eu_service_name
  location: 'East US'
  kind: 'app'
  properties: {
    enabled: true
    hostNameSslStates: [
      {
        name: '${sites_mandm_prod_eu_service_name}.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: '${sites_mandm_prod_eu_service_name}.scm.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Repository'
      }
    ]
    serverFarmId: serverfarms_ssnzwinserviceplan_name_resource.id
    reserved: false
    isXenon: false
    hyperV: false
    siteConfig: {
      numberOfWorkers: 1
      acrUseManagedIdentityCreds: false
      alwaysOn: true
      http20Enabled: false
      functionAppScaleLimit: 0
      minimumElasticInstanceCount: 0
    }
    scmSiteAlsoStopped: false
    clientAffinityEnabled: true
    clientCertEnabled: false
    clientCertMode: 'Required'
    hostNamesDisabled: false
    customDomainVerificationId: '94F391F7B468D7216F02F51D8356390A89B6F3949B2E9888D070424D1169AF7C'
    containerSize: 0
    dailyMemoryTimeQuota: 0
    keyVaultReferenceIdentity: 'SystemAssigned'
    httpsOnly: false
    redundancyMode: 'None'
    storageAccountRequired: false
  }
}

resource sites_ssnz_prod_eu_service_name_resource 'Microsoft.Web/sites@2021-01-15' = {
  name: sites_ssnz_prod_eu_service_name
  location: 'East US'
  kind: 'app'
  properties: {
    enabled: true
    hostNameSslStates: [
      {
        name: '${sites_ssnz_prod_eu_service_name}.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: '${sites_ssnz_prod_eu_service_name}.scm.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Repository'
      }
    ]
    serverFarmId: serverfarms_ssnzwinserviceplan_name_resource.id
    reserved: false
    isXenon: false
    hyperV: false
    siteConfig: {
      numberOfWorkers: 1
      acrUseManagedIdentityCreds: false
      alwaysOn: true
      http20Enabled: false
      functionAppScaleLimit: 0
      minimumElasticInstanceCount: 0
    }
    scmSiteAlsoStopped: false
    clientAffinityEnabled: true
    clientCertEnabled: false
    clientCertMode: 'Required'
    hostNamesDisabled: false
    customDomainVerificationId: '94F391F7B468D7216F02F51D8356390A89B6F3949B2E9888D070424D1169AF7C'
    containerSize: 0
    dailyMemoryTimeQuota: 0
    keyVaultReferenceIdentity: 'SystemAssigned'
    httpsOnly: false
    redundancyMode: 'None'
    storageAccountRequired: false
  }
}

resource sites_ssnz_prod_eu_web_name_resource 'Microsoft.Web/sites@2021-01-15' = {
  name: sites_ssnz_prod_eu_web_name
  location: 'East US'
  kind: 'app'
  properties: {
    enabled: true
    hostNameSslStates: [
      {
        name: 'samsmithnz.com'
        sslState: 'SniEnabled'
        thumbprint: '3DDAD621152BC15EEEBB819CEEA518733020D124'
        hostType: 'Standard'
      }
      {
        name: '${sites_ssnz_prod_eu_web_name}.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: 'www.samsmithnz.com'
        sslState: 'SniEnabled'
        thumbprint: '6F3048B10BDE4AA954BEE53B72C5FC8A00AA7E67'
        hostType: 'Standard'
      }
      {
        name: '${sites_ssnz_prod_eu_web_name}.scm.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Repository'
      }
    ]
    serverFarmId: serverfarms_ssnzwinserviceplan_name_resource.id
    reserved: false
    isXenon: false
    hyperV: false
    siteConfig: {
      numberOfWorkers: 1
      acrUseManagedIdentityCreds: false
      alwaysOn: true
      http20Enabled: false
      functionAppScaleLimit: 0
      minimumElasticInstanceCount: 0
    }
    scmSiteAlsoStopped: false
    clientAffinityEnabled: true
    clientCertEnabled: false
    clientCertMode: 'Required'
    hostNamesDisabled: false
    customDomainVerificationId: '94F391F7B468D7216F02F51D8356390A89B6F3949B2E9888D070424D1169AF7C'
    containerSize: 0
    dailyMemoryTimeQuota: 0
    keyVaultReferenceIdentity: 'SystemAssigned'
    httpsOnly: true
    redundancyMode: 'None'
    storageAccountRequired: false
  }
}

resource sites_mandm_prod_eu_service_name_web 'Microsoft.Web/sites/config@2021-01-15' = {
  parent: sites_mandm_prod_eu_service_name_resource
  name: 'web'
  location: 'East US'
  properties: {
    numberOfWorkers: 1
    defaultDocuments: [
      'Default.htm'
      'Default.html'
      'Default.asp'
      'index.htm'
      'index.html'
      'iisstart.htm'
      'default.aspx'
      'index.php'
      'hostingstart.html'
    ]
    netFrameworkVersion: 'v6.0'
    requestTracingEnabled: false
    remoteDebuggingEnabled: false
    httpLoggingEnabled: false
    acrUseManagedIdentityCreds: false
    logsDirectorySizeLimit: 35
    detailedErrorLoggingEnabled: false
    publishingUsername: '$mandm-prod-eu-service'
    azureStorageAccounts: {}
    scmType: 'VSTSRM'
    use32BitWorkerProcess: true
    webSocketsEnabled: false
    alwaysOn: true
    managedPipelineMode: 'Integrated'
    virtualApplications: [
      {
        virtualPath: '/'
        physicalPath: 'site\\wwwroot'
        preloadEnabled: true
      }
    ]
    loadBalancing: 'LeastRequests'
    experiments: {
      rampUpRules: []
    }
    autoHealEnabled: false
    vnetRouteAllEnabled: false
    vnetPrivatePortsCount: 0
    localMySqlEnabled: false
    ipSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 1
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 1
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictionsUseMain: false
    http20Enabled: false
    minTlsVersion: '1.2'
    scmMinTlsVersion: '1.0'
    ftpsState: 'AllAllowed'
    preWarmedInstanceCount: 0
    functionAppScaleLimit: 0
    functionsRuntimeScaleMonitoringEnabled: false
    minimumElasticInstanceCount: 0
  }
}

resource sites_ssnz_prod_eu_service_name_web 'Microsoft.Web/sites/config@2021-01-15' = {
  parent: sites_ssnz_prod_eu_service_name_resource
  name: 'web'
  location: 'East US'
  properties: {
    numberOfWorkers: 1
    defaultDocuments: [
      'Default.htm'
      'Default.html'
      'Default.asp'
      'index.htm'
      'index.html'
      'iisstart.htm'
      'default.aspx'
      'index.php'
      'hostingstart.html'
    ]
    netFrameworkVersion: 'v6.0'
    requestTracingEnabled: false
    remoteDebuggingEnabled: false
    httpLoggingEnabled: false
    acrUseManagedIdentityCreds: false
    logsDirectorySizeLimit: 35
    detailedErrorLoggingEnabled: false
    publishingUsername: '$ssnz-prod-eu-service'
    azureStorageAccounts: {}
    scmType: 'VSTSRM'
    use32BitWorkerProcess: false
    webSocketsEnabled: false
    alwaysOn: true
    managedPipelineMode: 'Integrated'
    virtualApplications: [
      {
        virtualPath: '/'
        physicalPath: 'site\\wwwroot'
        preloadEnabled: true
      }
    ]
    loadBalancing: 'LeastRequests'
    experiments: {
      rampUpRules: []
    }
    autoHealEnabled: false
    vnetRouteAllEnabled: false
    vnetPrivatePortsCount: 0
    localMySqlEnabled: false
    ipSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 1
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 1
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictionsUseMain: false
    http20Enabled: false
    minTlsVersion: '1.2'
    scmMinTlsVersion: '1.0'
    ftpsState: 'AllAllowed'
    preWarmedInstanceCount: 0
    functionAppScaleLimit: 0
    functionsRuntimeScaleMonitoringEnabled: false
    minimumElasticInstanceCount: 0
  }
}

resource sites_ssnz_prod_eu_web_name_web 'Microsoft.Web/sites/config@2021-01-15' = {
  parent: sites_ssnz_prod_eu_web_name_resource
  name: 'web'
  location: 'East US'
  properties: {
    numberOfWorkers: 1
    defaultDocuments: [
      'Default.htm'
      'Default.html'
      'Default.asp'
      'index.htm'
      'index.html'
      'iisstart.htm'
      'default.aspx'
      'index.php'
      'hostingstart.html'
    ]
    netFrameworkVersion: 'v6.0'
    requestTracingEnabled: false
    remoteDebuggingEnabled: false
    httpLoggingEnabled: false
    acrUseManagedIdentityCreds: false
    logsDirectorySizeLimit: 35
    detailedErrorLoggingEnabled: false
    publishingUsername: '$ssnz-prod-eu-web'
    azureStorageAccounts: {}
    scmType: 'None'
    use32BitWorkerProcess: false
    webSocketsEnabled: false
    alwaysOn: true
    managedPipelineMode: 'Integrated'
    virtualApplications: [
      {
        virtualPath: '/'
        physicalPath: 'site\\wwwroot'
        preloadEnabled: true
      }
    ]
    loadBalancing: 'LeastRequests'
    experiments: {
      rampUpRules: []
    }
    autoHealEnabled: false
    vnetRouteAllEnabled: false
    vnetPrivatePortsCount: 0
    localMySqlEnabled: false
    ipSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 1
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 1
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictionsUseMain: false
    http20Enabled: false
    minTlsVersion: '1.2'
    scmMinTlsVersion: '1.0'
    ftpsState: 'AllAllowed'
    preWarmedInstanceCount: 0
    functionAppScaleLimit: 0
    functionsRuntimeScaleMonitoringEnabled: false
    minimumElasticInstanceCount: 0
  }
}

resource vaults_ssnzkeyvault_name_resource 'Microsoft.KeyVault/vaults@2021-04-01-preview' = {
  name: vaults_ssnzkeyvault_name
  location: 'eastus'
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: '40244aae-8aee-4af5-b221-b480db24698b'
    accessPolicies: [
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
    enabledForDeployment: false
    enabledForDiskEncryption: false
    enabledForTemplateDeployment: false
    vaultUri: 'https://${vaults_ssnzkeyvault_name}.vault.azure.net/'
    provisioningState: 'Succeeded'
  }
}

resource storageAccounts_ssnzdbserverlogstorage_name_resource 'Microsoft.Storage/storageAccounts@2021-04-01' = {
  name: storageAccounts_ssnzdbserverlogstorage_name
  location: 'eastus'
  sku: {
    name: 'Standard_LRS'
    tier: 'Standard'
  }
  kind: 'StorageV2'
  properties: {
    networkAcls: {
      bypass: 'AzureServices'
      virtualNetworkRules: []
      ipRules: []
      defaultAction: 'Allow'
    }
    supportsHttpsTrafficOnly: false
    encryption: {
      services: {
        file: {
          keyType: 'Account'
          enabled: true
        }
        blob: {
          keyType: 'Account'
          enabled: true
        }
      }
      keySource: 'Microsoft.Storage'
    }
    accessTier: 'Hot'
  }
}

resource storageAccounts_ssnzdbserverlogstorage_name_default 'Microsoft.Storage/storageAccounts/blobServices@2021-04-01' = {
  parent: storageAccounts_ssnzdbserverlogstorage_name_resource
  name: 'default'
  sku: {
    name: 'Standard_LRS'
    tier: 'Standard'
  }
  properties: {
    cors: {
      corsRules: []
    }
    deleteRetentionPolicy: {
      enabled: false
    }
  }
}

resource Microsoft_Storage_storageAccounts_queueServices_storageAccounts_ssnzdbserverlogstorage_name_default 'Microsoft.Storage/storageAccounts/queueServices@2021-04-01' = {
  parent: storageAccounts_ssnzdbserverlogstorage_name_resource
  name: 'default'
  properties: {
    cors: {
      corsRules: []
    }
  }
}

resource Microsoft_Storage_storageAccounts_tableServices_storageAccounts_ssnzdbserverlogstorage_name_default 'Microsoft.Storage/storageAccounts/tableServices@2021-04-01' = {
  parent: storageAccounts_ssnzdbserverlogstorage_name_resource
  name: 'default'
  properties: {
    cors: {
      corsRules: []
    }
  }
}

resource storageAccounts_ssnzdbserverlogstorage_name_default_azure_jobs_host_output 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-04-01' = {
  parent: storageAccounts_ssnzdbserverlogstorage_name_default
  name: 'azure-jobs-host-output'
  properties: {
    defaultEncryptionScope: '$account-encryption-key'
    denyEncryptionScopeOverride: false
    publicAccess: 'None'
  }
  dependsOn: [
    storageAccounts_ssnzdbserverlogstorage_name_resource
  ]
}

resource storageAccounts_ssnzdbserverlogstorage_name_default_azure_webjobs_hosts 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-04-01' = {
  parent: storageAccounts_ssnzdbserverlogstorage_name_default
  name: 'azure-webjobs-hosts'
  properties: {
    defaultEncryptionScope: '$account-encryption-key'
    denyEncryptionScopeOverride: false
    publicAccess: 'None'
  }
  dependsOn: [
    storageAccounts_ssnzdbserverlogstorage_name_resource
  ]
}

resource storageAccounts_ssnzdbserverlogstorage_name_default_letsencrypt 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-04-01' = {
  parent: storageAccounts_ssnzdbserverlogstorage_name_default
  name: 'letsencrypt'
  properties: {
    defaultEncryptionScope: '$account-encryption-key'
    denyEncryptionScopeOverride: false
    publicAccess: 'None'
  }
  dependsOn: [
    storageAccounts_ssnzdbserverlogstorage_name_resource
  ]
}

resource storageAccounts_ssnzdbserverlogstorage_name_default_sqldbauditlogs 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-04-01' = {
  parent: storageAccounts_ssnzdbserverlogstorage_name_default
  name: 'sqldbauditlogs'
  properties: {
    defaultEncryptionScope: '$account-encryption-key'
    denyEncryptionScopeOverride: false
    publicAccess: 'None'
  }
  dependsOn: [
    storageAccounts_ssnzdbserverlogstorage_name_resource
  ]
}
