@echo ogg
echo copie des sources de MokaDocos
chcp 65001
set Msbuildexe="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe"
set Nugetexe="D:\DevCenter\Tools\nuget.exe"

%Msbuildexe% "D:\DevCenter\Sources\MokaDocos\MokaDocosOutilsBuild\MokaDocos01.xml" /nologo  >%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur
%Msbuildexe% "D:\DevCenter\Sources\MokaDocos\MokaDocosOutilsBuild\MokaDocos02.xml" >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur
xcopy "D:\DevCenter\Sources\MokaDocos\MokaDocosOutilsBuild\MokaDocos.nuspec" "D:\DevCenter\build\TempBuild\bin\MokaDocos\0.1.1\" /y >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur
xcopy "D:\DevCenter\Sources\MokaDocos\MokaDocosOutilsBuild\MokaDocosNconfig.xml" "D:\DevCenter\build\TempBuild\bin\MokaDocos\0.1.1\" /y >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur

echo creer la config
%Nugetexe% config -set verbosity=detailed -configfile "D:\DevCenter\build\TempBuild\bin\MokaDocos\0.1.1\MokaDocosNConfig.xml" >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreurs

%Nugetexe% pack "D:\DevCenter\build\TempBuild\bin\MokaDocos\0.1.1\MokaDocos.nuspec" -OutputDirectory "D:\DevCenter\Nuget\MaGallerie"  -configfile "D:\DevCenter\build\TempBuild\bin\MokaDocos\0.1.1\MokaDocosNConfig.xml" >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreurs

goto fini
:erreurs
pause
:erreur
echo Fichier de command fini avec erreur >>%~dp0log.txt
notepad %~dp0log.txt
:fini
echo Fichier de command fini sans erreur >>%~dp0log.txt
