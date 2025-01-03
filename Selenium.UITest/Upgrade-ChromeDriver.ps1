$projects = Get-ChildItem -Recurse *.csproj
foreach ($project in $projects) {
  cd $project.Directory
  Write-Host "Updating package in $($project.Directory)..."
  dotnet add package Selenium.WebDriver.ChromeDriver
  Write-Host "Done!"
  Write-Host "--"
}