properties {
  $projectName = "Net.Http.WebApi.OData"
  $baseDir = Resolve-Path .
  $buildDir = "$baseDir\build"
  $buildDir40 = "$buildDir\4.0\"
  $buildDir45 = "$buildDir\4.5\"
}

Task Default -depends RunTests, Build40, Build45

Task Build40 {
  Remove-Item -force -recurse $buildDir40 -ErrorAction SilentlyContinue
  
  Write-Host "Building $projectName.csproj for .net 4.0" -ForegroundColor Green
  Exec { msbuild "$projectName\$projectName.csproj" /target:Rebuild "/property:Configuration=Release;DefineConstants=NET_4_0;OutDir=$buildDir40;TargetFrameworkVersion=v4.0" /verbosity:quiet }
}

Task Build45 {
  Remove-Item -force -recurse $buildDir45 -ErrorAction SilentlyContinue
  
  Write-Host "Building $projectName.csproj for .net 4.5" -ForegroundColor Green
  Exec { msbuild "$projectName\$projectName.csproj" /target:Rebuild "/property:Configuration=Release;DefineConstants=NET_4_5;OutDir=$buildDir45;TargetFrameworkVersion=v4.5" /verbosity:quiet }
}

Task RunTests -Depends Build {
  Write-Host "Running $projectName.Tests" -ForegroundColor Green
  Exec {  & $baseDir\packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe "$baseDir\$projectName.Tests\bin\Release\$projectName.Tests.dll" }
}

Task Build -Depends Clean {
  Write-Host "Building $projectName.sln" -ForegroundColor Green
  Exec { msbuild "$projectName.sln" /target:Build /property:Configuration=Release /verbosity:quiet }  
}

Task Clean {
  Write-Host "Cleaning $projectName.sln" -ForegroundColor Green
  Exec { msbuild "$projectName.sln" /t:Clean /p:Configuration=Release /v:quiet }
}