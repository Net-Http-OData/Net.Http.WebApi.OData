properties {
  $projectName = "Net.Http.WebApi.OData"
  $baseDir = Resolve-Path .
  $buildDir = "$baseDir\build"

  $builds = @(
    @{Name = "NET40"; Constants="NET_4_0"; BuildDir="$buildDir\4.0\"; Framework="v4.0"},
    @{Name = "NET45"; Constants="NET_4_5"; BuildDir="$buildDir\4.5\"; Framework="v4.5"},
    @{Name = "NET46"; Constants="NET_4_6"; BuildDir="$buildDir\4.6\"; Framework="v4.6"}
  )
}

Task Default -depends RunTests

Task Clean {
  Write-Host "Cleaning $projectName Build Directory" -ForegroundColor Green
  foreach ($build in $builds) {
    $outDir = $build.BuildDir
    Remove-Item -force -recurse $outDir -ErrorAction SilentlyContinue
  }
  Write-Host
}

Task Build -Depends Clean {
  foreach ($build in $builds) {
    $name = $build.Name
    Write-Host "Building $projectName.$name.sln" -ForegroundColor Green

    $constants = $build.Constants
    $outDir = $build.BuildDir
    $netVer = $build.Framework
    Exec { msbuild "$projectName.$name.sln" "/target:Clean;Rebuild" "/property:Configuration=Release;WarningLevel=1;DefineConstants=$constants;OutDir=$outDir;TargetFrameworkVersion=$netVer" /verbosity:quiet }
  }
  Write-Host
}

Task RunTests -Depends Build {
  foreach ($build in $builds) {
    $name = $build.Name
    Write-Host "Running $projectName.Tests.$name" -ForegroundColor Green

    $outDir = $build.BuildDir
    Exec {  & $baseDir\packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe "$outDir\$projectName.Tests.dll" }
  }
  Write-Host
}