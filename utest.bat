@ECHO OFF
cd /d %~dp0

if exist TestResult.xml del /Q /F TestResult.xml

SET NUNIT=.\packages\NUnit.ConsoleRunner.3.2.0\tools\nunit3-console.exe

%NUNIT% Slocker.nunit
