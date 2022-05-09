@echo off
setlocal

cd "%~dp0"

mkdir bld\dist\

For %%a in (
"XwaSalvageYardScoreEditor\XwaSalvageYardScoreEditor\bin\Release\net48\*.exe"
"XwaSalvageYardScoreEditor\XwaSalvageYardScoreEditor\bin\Release\net48\*.exe.config"
) do (
xcopy /s /d "%%~a" bld\dist\
)
