// AUTO-GENERATED FILE - FOR REFERENCE ONLY
// This file was automatically decompiled from the original ARM template.
// The production-ready modular database file is:
//   - modules/database.bicep
// 
// Use the modular file for deployments, not this auto-generated file.

param servers_ssnzdbserver_name string = 'ssnzdbserver'

resource servers_ssnzdbserver_name_resource 'Microsoft.Sql/servers@2023-08-01-preview' = {
  name: servers_ssnzdbserver_name
  location: 'eastus'
  kind: 'v12.0'
  properties: {
    administratorLogin: 'ssnzadmin'
    version: '12.0'
    publicNetworkAccess: 'Enabled'
    restrictOutboundNetworkAccess: 'Disabled'
  }
}

resource servers_ssnzdbserver_name_SSNZDB 'Microsoft.Sql/servers/databases@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'SSNZDB'
  location: 'eastus'
  sku: {
    name: 'Basic'
    tier: 'Basic'
    capacity: 5
  }
  kind: 'v12.0,user'
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

resource servers_ssnzdbserver_name_150SouthPoint 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: '150SouthPoint'
  properties: {
    startIpAddress: '157.58.214.1'
    endIpAddress: '157.58.214.255'
  }
}

resource servers_ssnzdbserver_name_58RedSpringRd 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: '58RedSpringRd'
  properties: {
    startIpAddress: '24.147.166.1'
    endIpAddress: '24.147.166.255'
  }
}

resource servers_ssnzdbserver_name_AllowAllWindowsAzureIps 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'AllowAllWindowsAzureIps'
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2018_10_31_16_29_9 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2018-10-31_16-29-9'
  properties: {
    startIpAddress: '205.153.95.177'
    endIpAddress: '205.153.95.177'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2018_11_13_12_14_35 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2018-11-13_12:14:35'
  properties: {
    startIpAddress: '50.226.60.146'
    endIpAddress: '50.226.60.146'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2018_11_13_8_19_31 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2018-11-13_8-19-31'
  properties: {
    startIpAddress: '167.220.148.251'
    endIpAddress: '167.220.148.251'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2018_11_14_03_33_19 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2018-11-14_03:33:19'
  properties: {
    startIpAddress: '167.220.148.35'
    endIpAddress: '167.220.148.35'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2018_11_21_01_38_44 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2018-11-21_01:38:44'
  properties: {
    startIpAddress: '167.220.148.128'
    endIpAddress: '167.220.148.128'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2018_11_21_01_46_56 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2018-11-21_01:46:56'
  properties: {
    startIpAddress: '167.220.149.128'
    endIpAddress: '167.220.149.128'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2018_11_24_03_51_11 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2018-11-24_03:51:11'
  properties: {
    startIpAddress: '174.62.193.13'
    endIpAddress: '174.62.193.13'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2018_11_26_02_34_24 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2018-11-26_02:34:24'
  properties: {
    startIpAddress: '96.237.112.134'
    endIpAddress: '96.237.112.134'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2018_12_31_8_57_39 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2018-12-31_8-57-39'
  properties: {
    startIpAddress: '204.13.47.215'
    endIpAddress: '204.13.47.215'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2020_10_22_20_46_9 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2020-10-22_20-46-9'
  properties: {
    startIpAddress: '157.58.212.1'
    endIpAddress: '157.58.212.255'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2020_10_23_8_29_11 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2020-10-23_8-29-11'
  properties: {
    startIpAddress: '72.74.50.1'
    endIpAddress: '72.74.50.255'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2020_11_25_12_57_53 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2020-11-25_12-57-53'
  properties: {
    startIpAddress: '96.237.112.20'
    endIpAddress: '96.237.112.20'
  }
}

resource servers_ssnzdbserver_name_ClientIPAddress_2021_7_2_16_22_8 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'ClientIPAddress_2021-7-2_16-22-8'
  properties: {
    startIpAddress: '71.235.107.1'
    endIpAddress: '71.235.107.255'
  }
}

resource servers_ssnzdbserver_name_FullMoonCafe 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'FullMoonCafe'
  properties: {
    startIpAddress: '167.220.148.0'
    endIpAddress: '167.220.148.255'
  }
}

resource servers_ssnzdbserver_name_FullMoonCafe2 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'FullMoonCafe2'
  properties: {
    startIpAddress: '167.220.149.0'
    endIpAddress: '167.220.149.255'
  }
}

resource servers_ssnzdbserver_name_MasterShin 'Microsoft.Sql/servers/firewallRules@2023-08-01-preview' = {
  parent: servers_ssnzdbserver_name_resource
  name: 'MasterShin'
  properties: {
    startIpAddress: '71.184.204.1'
    endIpAddress: '71.184.204.255'
  }
}
