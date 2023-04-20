@echo off

for /f "usebackq tokens=*" %%i in (`vswhere -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath`) do (
  set InstallDir=%%i
)

if exist "%InstallDir%\Common7\Tools\vsdevcmd.bat" (
  "%InstallDir%\Common7\Tools\vsdevcmd.bat" 
  cd %~dp0\..\Source\Raylib-CSharp-Vinculum.ExampleCore
  MSBuild Raylib-CSharp-Vinculum.ExampleCore.csproj -restore -target:rebuild -property:Configuration=Release
)