@echo off
REM w64/check_cli.bat
REM
REM This batch file checks that GLPK can be used with C#.
REM C# examples in directory ..\examples are built and executed.
REM @author Heinrich Schuchardt, 2015



set HOME="C:\Program Files (x86)\Microsoft Visual Studio 14.0\"
set NET="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\"

set PATH=%NET%;%PATH%


csc.exe /r:libglpk-cli.dll ..\examples\csharp\lp.cs
lp.exe
