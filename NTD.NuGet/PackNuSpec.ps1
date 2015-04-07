Write-Host "Search for .nuspec files ..."
$name = Get-ChildItem -Filter *.nuspec -Recurse
Write-Host "Found: " $name " !"
./NuGet.exe pack $name

Try
{
	copy-item *.nupkg D:\Project\NuGet_server
	Write-Host "Copied " $name " files."
}
Catch [system.exception]
{
	Write-Host "Error copied files."
}

Read-Host -Prompt "Press Enter to exit"