dotnet publish --configuration Release
cd TramBoard.WEB\bin\Release\net6.0\publish
Compress-Archive -Force -Path * -DestinationPath deploy.zip
Publish-AzWebApp -ResourceGroupName tram-board -Name tram-board -ArchivePath (Get-Item .\deploy.zip).FullName -Force
Read-Host -Prompt "Press Enter to exit"