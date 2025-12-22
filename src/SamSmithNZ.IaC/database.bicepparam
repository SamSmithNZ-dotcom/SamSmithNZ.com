using './modules/database.bicep'

// Parameters for database deployment
param sqlServerName = 'ssnzdbserver'
param location = 'eastus'
param administratorLogin = 'ssnzadmin'
param databaseName = 'SSNZDB'

// Firewall rules
param firewallRules = [
  {
    name: '150SouthPoint'
    startIpAddress: '157.58.214.1'
    endIpAddress: '157.58.214.255'
  }
  {
    name: '58RedSpringRd'
    startIpAddress: '24.147.166.1'
    endIpAddress: '24.147.166.255'
  }
  {
    name: 'AllowAllWindowsAzureIps'
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
  {
    name: 'ClientIPAddress_2018-10-31_16-29-9'
    startIpAddress: '205.153.95.177'
    endIpAddress: '205.153.95.177'
  }
  {
    name: 'ClientIPAddress_2018-11-13_12:14:35'
    startIpAddress: '50.226.60.146'
    endIpAddress: '50.226.60.146'
  }
  {
    name: 'ClientIPAddress_2018-11-13_8-19-31'
    startIpAddress: '167.220.148.251'
    endIpAddress: '167.220.148.251'
  }
  {
    name: 'ClientIPAddress_2018-11-14_03:33:19'
    startIpAddress: '167.220.148.35'
    endIpAddress: '167.220.148.35'
  }
  {
    name: 'ClientIPAddress_2018-11-21_01:38:44'
    startIpAddress: '167.220.148.128'
    endIpAddress: '167.220.148.128'
  }
  {
    name: 'ClientIPAddress_2018-11-21_01:46:56'
    startIpAddress: '167.220.149.128'
    endIpAddress: '167.220.149.128'
  }
  {
    name: 'ClientIPAddress_2018-11-24_03:51:11'
    startIpAddress: '174.62.193.13'
    endIpAddress: '174.62.193.13'
  }
  {
    name: 'ClientIPAddress_2018-11-26_02:34:24'
    startIpAddress: '96.237.112.134'
    endIpAddress: '96.237.112.134'
  }
  {
    name: 'ClientIPAddress_2018-12-31_8-57-39'
    startIpAddress: '204.13.47.215'
    endIpAddress: '204.13.47.215'
  }
  {
    name: 'ClientIPAddress_2020-10-22_20-46-9'
    startIpAddress: '157.58.212.1'
    endIpAddress: '157.58.212.255'
  }
  {
    name: 'ClientIPAddress_2020-10-23_8-29-11'
    startIpAddress: '72.74.50.1'
    endIpAddress: '72.74.50.255'
  }
  {
    name: 'ClientIPAddress_2020-11-25_12-57-53'
    startIpAddress: '96.237.112.20'
    endIpAddress: '96.237.112.20'
  }
  {
    name: 'ClientIPAddress_2021-7-2_16-22-8'
    startIpAddress: '71.235.107.1'
    endIpAddress: '71.235.107.255'
  }
  {
    name: 'FullMoonCafe'
    startIpAddress: '167.220.148.0'
    endIpAddress: '167.220.148.255'
  }
  {
    name: 'FullMoonCafe2'
    startIpAddress: '167.220.149.0'
    endIpAddress: '167.220.149.255'
  }
  {
    name: 'MasterShin'
    startIpAddress: '71.184.204.1'
    endIpAddress: '71.184.204.255'
  }
]
