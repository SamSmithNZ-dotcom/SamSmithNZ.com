##############################################
#Copy files source location to target location
##############################################

param
(
	[string] $sourceLocation,
	[string] $targetLocation
)
Write-Output "Entering script CopyFiles.ps1"
Write-Output "Version 1.00" #Initial Release

#Write-Output "PowerShell testing"
#Write-Output "This is an example of passing a variable as a parameter to the PowerShell script, utilizing the param(eter). backupSourceLocation: $backupSourceLocation"
#Write-Output "This is an example of using a predefined variable environment variable from the build variables. BuildConfiguration: $Env:BuildConfiguration $env:BuildConfiguration"
#Write-Output "All done PowerShell testing!"

Write-Output "Copying file from '$sourceLocation' to '$targetLocation'"
Copy-Item $sourceLocation $targetLocation

Write-Output "Exiting script CopyFiles.ps1"