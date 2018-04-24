taskkill /f /im WindowsApp2.exe
SLEEP 3
del "WindowsApp2.exe"
SLEEP 2
ren "update.exe" "WindowsApp2.exe"
SLEEP 2
start /d WindowsApp2.exe