copy bin\Debug\dn3.5\en-US\Perspective.resources.dll .
copy bin\Debug\dn3.5\Perspective.exe .
LocBaml /parse Perspective.resources.dll /out:Resources.en-US.csv
if exist Perspective.resources.dll del Perspective.resources.dll
if exist Perspective.exe del Perspective.exe
pause