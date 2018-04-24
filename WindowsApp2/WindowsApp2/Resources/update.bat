@echo off
taskkill /im WindowsApp2.exe 2> nul
del "WindowsApp2.exe"
ren "update.exe" "WindowsApp2.exe"