taskkill /f /im WindowsApp2.exe
del "WindowsApp2.exe"
ren "update.exe" "WindowsApp2.exe"
start /d WindowsApp2.exe