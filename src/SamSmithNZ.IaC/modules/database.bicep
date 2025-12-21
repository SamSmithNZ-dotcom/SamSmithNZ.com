// Database module
@description('Name of the SQL Server')
param sqlServerName string = 'ssnzdbserver'

@description('Location for the SQL Server')
param location string = 'eastus'

@description('SQL Server administrator login')
param administratorLogin string = 'ssnzadmin'

@description('SQL Server administrator password')
@secure()
param administratorLoginPassword string

@description('Name of the SQL Database')
param databaseName string = 'SSNZDB'

@description('Firewall rules for SQL Server')
param firewallRules array = []

resource sqlServer 'Microsoft.Sql/servers@2023-08-01-preview' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
    version: '12.0'
    publicNetworkAccess: 'Enabled'
    restrictOutboundNetworkAccess: 'Disabled'
  }
}

resource sqlDatabase 'Microsoft.Sql/servers/databases@2023-08-01-preview' = {
  parent: sqlServer
  name: databaseName
  location: location
  sku: {
    name: 'Basic'
    tier: 'Basic'
    capacity: 5
  }
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    maxSizeBytes: 2147483648
    catalogCollation: 'SQL_Latin1_General_CP1_CI_AS'
    zoneRedundant: false
    readScale: 'Disabled'
    requestedBackupStorageRedundancy: 'Geo'
    maintenanceConfigurationId: '/subscriptions/65b8d298-e5bd-4735-912e-8b9c510c4e00/providers/Microsoft.Maintenance/publicMaintenanceConfigurations/SQL_Default'
    isLedgerOn: false
  }
}

// Create firewall rules dynamically
resource firewallRule 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = [for rule in firewallRules: {
  parent: sqlServer
  name: rule.name
  properties: {
    startIpAddress: rule.startIpAddress
    endIpAddress: rule.endIpAddress
  }
}]

output sqlServerId string = sqlServer.id
output sqlServerName string = sqlServer.name
output sqlDatabaseId string = sqlDatabase.id
output sqlDatabaseName string = sqlDatabase.name
