@echo off
setlocal
pushd "%~dp0"
:loop
echo Starting dotnet run in %CD%...
dotnet run
echo.
choice /C RQ /N /M "Press R to restart or Q to quit: "
if errorlevel 2 (
  echo Quitting.
  popd
  exit /b 0
)
if errorlevel 1 (
  echo Restarting...
  goto loop
)
goto loop
