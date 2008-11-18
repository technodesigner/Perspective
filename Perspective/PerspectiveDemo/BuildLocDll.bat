@echo off

copy bin\Debug\dn3.5\en-US\Perspective.resources.dll . >nul
copy bin\Debug\dn3.5\Perspective.exe . >nul
LocBaml /generate Perspective.resources.dll /trans:Resources.fr.csv /out:bin\Debug\dn3.5\fr /cul:fr
if exist Perspective.resources.dll del Perspective.resources.dll >nul
if exist Perspective.exe del Perspective.exe >nul

copy bin\Debug\dn3.0\en-US\Perspective.resources.dll . >nul
copy bin\Debug\dn3.0\Perspective.exe . >nul
LocBaml /generate Perspective.resources.dll /trans:Resources.fr.csv /out:bin\Debug\dn3.0\fr /cul:fr
if exist Perspective.resources.dll del Perspective.resources.dll >nul
if exist Perspective.exe del Perspective.exe >nul

copy bin\Release\dn3.5\en-US\Perspective.resources.dll . >nul
copy bin\Release\dn3.5\Perspective.exe . >nul
LocBaml /generate Perspective.resources.dll /trans:Resources.fr.csv /out:bin\Release\dn3.5\fr /cul:fr
if exist Perspective.resources.dll del Perspective.resources.dll >nul
if exist Perspective.exe del Perspective.exe >nul

copy bin\Release\dn3.0\en-US\Perspective.resources.dll . >nul
copy bin\Release\dn3.0\Perspective.exe . >nul
LocBaml /generate Perspective.resources.dll /trans:Resources.fr.csv /out:bin\Release\dn3.0\fr /cul:fr
if exist Perspective.resources.dll del Perspective.resources.dll >nul
if exist Perspective.exe del Perspective.exe >nul


pause