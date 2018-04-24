@echo off
taskkill /f /im WindowsApp2.exe
TIMEOUT 2
del "WindowsApp2.exe"
TIMEOUT 2
ren "update.exe" "WindowsApp2.exe"
TIMEOUT 2
start /d "%cd%" WindowsApp2.exe
