#############################
#Restore database from backup
#############################
param
(
	[string] $sqlServerName,
	[string] $sqlDatabaseName,
	[string] $sqlFullBackupLocation,
	[string] $sqlDiffBackupLocation
)
Write-Output "Entering script RestoreDatabaseBackup.ps1"
Write-Output "Version 1.02" #Updated to handle databases that are locked in single user 
#Write-Output "Version 1.01" #Updated to handle servers without the database
#Write-Output "Version 1.00" #Initial Release

Write-Output "Restoring database $sqlDatabaseName to '$sqlFullBackupLocation', on the server $sqlServerName"
if ($sqlDiffBackupLocation -ne "")
{
	Write-Output "Restoring database $sqlDatabaseName to '$sqlDiffBackupLocation', on the server $sqlServerName"
}

#Create a connection and run our scripts, assuming that this is running as Bainadmin_sql. We use the master database so there is no conflict when restoring
$conn = New-Object System.Data.SqlClient.SqlConnection
$conn.ConnectionString = "Data Source=$sqlServerName;Initial Catalog=master;Integrated Security=true;"
$conn.open()
$cmd = New-Object System.Data.SqlClient.SqlCommand
$cmd.connection = $conn

#alter database AppStack set single_user with rollback immediate
Write-Output "IF (EXISTS(SELECT name FROM sysdatabases WHERE name = '$sqlDatabaseName') AND (SELECT user_access_desc FROM sys.databases WHERE name = '$sqlDatabaseName') <> 'SINGLE_USER' AND DATABASEPROPERTYEX ('appstack', 'Status') <> 'RESTORING') alter database $sqlDatabaseName set single_user with rollback immediate"
$cmd.commandtext = "IF (EXISTS(SELECT name FROM sysdatabases WHERE name = '$sqlDatabaseName') AND (SELECT user_access_desc FROM sys.databases WHERE name = '$sqlDatabaseName') <> 'SINGLE_USER' AND DATABASEPROPERTYEX ('appstack', 'Status') <> 'RESTORING') alter database $sqlDatabaseName set single_user with rollback immediate" 
$cmd.executenonquery()

#restore database AppStack from disk = 'e:\mssql11.mssqlserver\mssql\backup\AppStack_full.bak' with replace, norecovery 
if ($sqlDiffBackupLocation -ne "")
{
	$cmd.commandtext = "restore database $sqlDatabaseName from disk = '$sqlFullBackupLocation' with replace, norecovery" 
}
else
{
	$cmd.commandtext = "restore database $sqlDatabaseName from disk = '$sqlFullBackupLocation' with replace, recovery" 
}
$cmd.executenonquery()

#restore database AppStack from disk = 'e:\mssql11.mssqlserver\mssql\backup\AppStack_diff.bak' with recovery 
if ($sqlDiffBackupLocation -ne "")
{
	$cmd.commandtext = "restore database $sqlDatabaseName from disk = '$sqlDiffBackupLocation' with recovery" 
	$cmd.executenonquery()
}

#Close the connection and clean up
$conn.close()

Write-Output "Exiting script RestoreDatabaseBackup.ps1"