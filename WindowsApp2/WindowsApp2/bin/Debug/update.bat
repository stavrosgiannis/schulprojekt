@echo off
taskkill /f /im WindowsApp2.exe
TIMEOUT 1
del "WindowsApp2.exe"
TIMEOUT 1
ren "update.exe" "WindowsApp2.exe"
TIMEOUT 1
start /d "%cd%" WindowsApp2.exe
