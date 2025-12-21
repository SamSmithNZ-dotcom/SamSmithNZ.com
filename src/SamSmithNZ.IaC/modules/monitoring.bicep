// Application Insights module
@description('Name of the Application Insights instance')
param appInsightsName string = 'ssnzapplicationinsights'

@description('Location for the Application Insights instance')
param location string = 'eastus'

resource appInsights 'microsoft.insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    Flow_Type: 'Brownfield'
    Request_Source: 'VSIX8.13.10627.1'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

output appInsightsId string = appInsights.id
output appInsightsInstrumentationKey string = appInsights.properties.InstrumentationKey
output appInsightsName string = appInsights.name
