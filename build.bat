@echo off

cd %~dp0\Build

Powershell.exe -ExecutionPolicy Unrestricted -File build-projects.ps1
 
call build-windows.bat

Powershell.exe -ExecutionPolicy Unrestricted -File build-autogen.ps1

call build-vinculum.bat
cd %~dp0\Build
call build-examples.bat

pause