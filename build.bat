@echo off
setlocal
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0build.ps1" -Target windows -Mono %*
set "buildExit=%errorlevel%"
if not "%buildExit%"=="0" if "%~1"=="" pause
exit /b %buildExit%
