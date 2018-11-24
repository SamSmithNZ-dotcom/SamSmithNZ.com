#############################
#Restore database from backup
#############################
param
(
	[string] $sqlServerName,
	[string] $sqlDatabaseName,
	[string] $sqlSourceFullBackupFile,
	[string] $sqlSourceDiffBackupFile,
	[string] $sqlDestinationFullBackupFile,
	[string] $sqlDestinationDiffBackupFile,
	[int] $sqlCommandTimeout = 30 #defaults to 30 seconds (the normal timeout for a command)
)

Write-Output "Entering script CopyAndRestoreDatabaseBackups.ps1"

Write-Output "Version 1.09" #Updated script to fix wrong database names in two of the database cross checks before the restore
#Write-Output "Version 1.08" #Updated script to have clearer parameter names and optional command timeout
#Write-Output "Version 1.07" #Updated script to work when the backup file doesn't need to be copied
#Write-Output "Version 1.06" #Updated script to work on Sunday's - when there is only a full backup and no differential backup
#Write-Output "Version 1.05" #Updated timeout to allow some databases restores to run for longer than 30 seconds
#Write-Output "Version 1.04" #Removed hardcoded database name in one of the scripts
#Write-Output "Version 1.03" #Refactored to merge in the copy file process to create just one SQL PowerShell file
#Write-Output "Version 1.02" #Updated to handle databases that are locked in single user 
#Write-Output "Version 1.01" #Updated to handle servers without the database
#Write-Output "Version 1.00" #Initial Release

#If the destination files are blank, set them to the destination file. This indicates we don't need to copy the backup file first
if ($sqlDestinationFullBackupFile -eq "")
{
    $sqlDestinationFullBackupFile = $sqlSourceFullBackupFile
}
if ($sqlDestinationDiffBackupFile -eq "")
{
    $sqlDestinationDiffBackupFile = $sqlSourceDiffBackupFile
}

#Copy the backup files (if they need to be copied)
if ($sqlSourceFullBackupFile -ne $sqlDestinationFullBackupFile)
{
	Write-Output "Copying file from '$sqlSourceFullBackupFile' to '$sqlDestinationFullBackupFile'"
	Copy-Item $sqlSourceFullBackupFile $sqlDestinationFullBackupFile
}
if ($sqlSourceDiffBackupFile -ne $sqlDestinationDiffBackupFile)
{
	Write-Output "Copying file from '$sqlSourceDiffBackupFile' to '$sqlDestinationDiffBackupFile'"
	Copy-Item $sqlSourceDiffBackupFile $sqlDestinationDiffBackupFile
}

#Write to the log
Write-Output "Restoring database $sqlDatabaseName to '$sqlDestinationFullBackupFile', on the server $sqlServerName"
if ($sqlDestinationDiffBackupFile -ne "")
{
	Write-Output "Restoring database $sqlDatabaseName to '$sqlDestinationDiffBackupFile', on the server $sqlServerName"
}

#Create a connection and run our scripts, assuming that this is running as Bainadmin_sql. We use the master database so there is no conflict when restoring
$conn = New-Object System.Data.SqlClient.SqlConnection
$conn.ConnectionString = "Data Source=$sqlServerName;Initial Catalog=master;Integrated Security=true;"
$conn.open()
$cmd = New-Object System.Data.SqlClient.SqlCommand
$cmd.connection = $conn
$cmd.commandTimeout = $sqlCommandTimeout #Set the timeout to 0 for large databases

#IF (EXISTS(SELECT name FROM sysdatabases WHERE name = '$sqlDatabaseName')
#	AND (SELECT user_access_desc FROM sys.databases WHERE name = '$sqlDatabaseName') <> 'SINGLE_USER'
#	AND DATABASEPROPERTYEX ('appstack', 'Status') <> 'RESTORING')
#alter database AppStack set single_user with rollback immediate
Write-Output "IF (EXISTS(SELECT name FROM sysdatabases WHERE name = '$sqlDatabaseName') AND (SELECT user_access_desc FROM sys.databases WHERE name = '$sqlDatabaseName') <> 'SINGLE_USER' AND DATABASEPROPERTYEX ('$sqlDatabaseName', 'Status') <> 'RESTORING') alter database $sqlDatabaseName set single_user with rollback immediate"
$cmd.commandtext = "IF (EXISTS(SELECT name FROM sysdatabases WHERE name = '$sqlDatabaseName') AND (SELECT user_access_desc FROM sys.databases WHERE name = '$sqlDatabaseName') <> 'SINGLE_USER' AND DATABASEPROPERTYEX ('$sqlDatabaseName', 'Status') <> 'RESTORING') alter database $sqlDatabaseName set single_user with rollback immediate" 
$cmd.executenonquery()

#restore database AppStack from disk = 'e:\mssql11.mssqlserver\mssql\backup\AppStack_full.bak' with replace, norecovery 
if ($sqlDestinationDiffBackupFile -ne "") #if the diff file is something, then we need to use norecovery, as after the full backup, the diff backup will finish off the backup
{
	Write-Output "restore database $sqlDatabaseName from disk = '$sqlDestinationFullBackupFile' with replace, norecovery" 
	$cmd.commandtext = "IF EXISTS(SELECT TOP 1 * FROM msdb.dbo.backupset bs WHERE bs.[database_name] = '$sqlDatabaseName' AND bs.[type] = 'D' AND NOT EXISTS (SELECT 1 FROM msdb.dbo.backupset bs2 WHERE bs2.[database_name] = '$sqlDatabaseName' AND bs2.[type] = 'I' AND bs2.backup_start_date > bs.backup_start_date) ORDER BY backup_start_date DESC) 
	
	BEGIN restore database $sqlDatabaseName from disk = '$sqlDestinationFullBackupFile' with replace, recovery END ELSE 
	
	BEGIN restore database $sqlDatabaseName from disk = '$sqlDestinationFullBackupFile' with replace, norecovery END" 
}
else #if the diff file is nothing, then use recover to finish off the backup
{
	Write-Output "restore database $sqlDatabaseName from disk = '$sqlDestinationFullBackupFile' with replace, recovery" 
	$cmd.commandtext = "restore database $sqlDatabaseName from disk = '$sqlDestinationFullBackupFile' with replace, recovery" 
}
$cmd.executenonquery()

#restore database AppStack from disk = 'e:\mssql11.mssqlserver\mssql\backup\AppStack_diff.bak' with recovery 
if ($sqlDestinationDiffBackupFile -ne "") #if the diff file is something, then we need to use recovery to finish off the backup
{
	Write-Output "restore database $sqlDatabaseName from disk = '$sqlDestinationDiffBackupFile' with recovery" 
	$cmd.commandtext = "IF EXISTS(SELECT TOP 1 * FROM msdb.dbo.backupset bs WHERE bs.[database_name] = '$sqlDatabaseName' AND bs.[type] = 'D' AND NOT EXISTS (SELECT 1 FROM msdb.dbo.backupset bs2 WHERE bs2.[database_name] = '$sqlDatabaseName' AND bs2.[type] = 'I' AND bs2.backup_start_date > bs.backup_start_date) ORDER BY backup_start_date DESC) BEGIN SELECT 0 END ELSE BEGIN restore database $sqlDatabaseName from disk = '$sqlDestinationDiffBackupFile' with recovery END" 
	$cmd.executenonquery()
}

#Close the connection and clean up
$conn.close()

Write-Output "Exiting script CopyAndRestoreDatabaseBackups.ps1"