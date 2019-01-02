@echo off
dir >%~dp0log.txt
xcopy %~dp0ModeleUTF8.txt %~dp0log.txt /y
if %errorlevel% NEQ 0 goto erreur

chcp 65001
echo  Exécution   %date% %time% >>%~dp0log.txt
echo copie des sources de DocDocos
chcp 65001
set Msbuildexe="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe"
set Nugetexe="D:\DevCenter\Tools\nuget.exe"
%Msbuildexe% "D:\DevCenter\Sources\DocDocos\DocDocosOutilsBuild\DocDocos01.xml" /nologo  >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur
%Msbuildexe% /help >>%~dp0log.txt

%Msbuildexe% "D:\DevCenter\Sources\DocDocos\DocDocosOutilsBuild\DocDocos02.xml" >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur
echo build D:\DevCenter\Sources\DocDocos\DocDocos\DocDocos.csproj >>%~dp0log.txt
%Msbuildexe% "D:\DevCenter\Sources\DocDocos\DocDocos\DocDocos.csproj" /p:Configuration=Release >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreurs
echo D:\DevCenter\Sources\DocDocos\DocDocos\bin\Release\netcoreapp2.1\DocDocos.dll
mkdir "D:\DevCenter\build\TempBuild\bin\DocDocos\0.1.1\lib\netcoreapp2.1" >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur

xcopy "D:\DevCenter\Sources\DocDocos\DocDocos\bin\Release\netcoreapp2.1\DocDocos.dll" "D:\DevCenter\build\TempBuild\bin\DocDocos\0.1.1\lib\netcoreapp2.1" /y>>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur


xcopy "D:\DevCenter\Sources\DocDocos\DocDocosOutilsBuild\DocDocos.nuspec" "D:\DevCenter\build\TempBuild\bin\DocDocos\0.1.1\" /f /y >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur
xcopy "D:\DevCenter\Sources\DocDocos\DocDocosOutilsBuild\DocDocosNconfig.xml" "D:\DevCenter\build\TempBuild\bin\DocDocos\0.1.1\" /y >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur
xcopy "D:\DevCenter\Sources\DocDocos\DocDocosOutilsBuild\DocDocos.props" "D:\DevCenter\build\TempBuild\bin\DocDocos\0.1.1\" /y >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur

echo creer la config
%Nugetexe% config -set verbosity=detailed -configfile "D:\DevCenter\build\TempBuild\bin\DocDocos\0.1.1\DocDocosNConfig.xml" >>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur

%Nugetexe% pack "D:\DevCenter\build\TempBuild\bin\DocDocos\0.1.1\DocDocos.nuspec" -OutputDirectory "D:\DevCenter\Nuget\MaGallerie"  -configfile "D:\DevCenter\build\TempBuild\bin\DocDocos\0.1.1\DocDocosNConfig.xml"2>>%~dp0log.txt
if %errorlevel% NEQ 0 goto erreur

goto fini
:erreurs
pause
:erreur
echo Fichier de command fini avec erreur >>%~dp0log.txt
notepad %~dp0log.txt
goto end
:fini
echo Fichier de command fini sans erreur >>%~dp0log.txt
:end