$project = "./YourSoundCompany.RelationalData\YourSoundCompany.RelationalData.csproj"
$startup_project = "./YourSoundCompany.Api\YourSoundCompany.Api.csproj"


Write-Host "****************************"
Write-Host "**                        **"
Write-Host "** Listando migrations... **"
Write-Host "**                        **"
Write-Host "****************************"
dotnet ef migrations list -p $project -s $startup_project

$initial = Read-Host "Digite o nome da migration inicial (default 0)"
$final = Read-Host "Digite o nome da migration final"

$outputName = "./"+$(Get-Date -Format "yyyy_MM_dd-HH_mm_ss") + "-Migration-$initial-to-$final.sql"
Write-Host "***********************************"
Write-Host "**                               **"
Write-Host "** Gerando SQL...                **"
Write-Host "** dotnet ef migrations script -p $project -s $startup_project -o $outputName $initial $final"
Write-Host "**                               **"
Write-Host "***********************************"
dotnet ef migrations script -p $project -s $startup_project -o $outputName $initial $final

Write-Host "SQL Gerado!"

Read-Host