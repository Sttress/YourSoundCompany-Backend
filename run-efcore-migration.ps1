$project = "./YourSoundCompany.RelationalData\YourSoundCompany.RelationalData.csproj"
$startup_project = "./YourSoundCompany.Api\YourSoundCompany.Api.csproj"

Write-Host "****************************"
Write-Host "**                        **"
Write-Host "** Listando migrations... **"
Write-Host "**                        **"
Write-Host "****************************"
dotnet ef migrations list -p $project -s $startup_project

$name = Read-Host "Digite o nome da migration"

Write-Host "************************"
Write-Host "**                    **"
Write-Host "** dotnet ef migrations add -p $project -s $startup_project $name"
Write-Host "**                    **"
Write-Host "************************"
dotnet ef migrations add -p $project -s $startup_project $name

Write-Host "************************"
Write-Host "**                    **"
Write-Host "** Migracao realizada **"
Write-Host "**                    **"
Write-Host "************************"

Read-Host