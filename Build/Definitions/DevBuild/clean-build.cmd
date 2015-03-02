@echo off
setlocal
REM Ensure we know whether we are running on a 32it or 64bit system
REM IF "%PROCESSOR_ARCHITECTURE%" == "x86" SET FRAMEWORKDIR=Framework
REM IF "%PROCESSOR_ARCHITECTURE%" == "AMD64" SET FRAMEWORKDIR=Framework64
SET MSBUILDEXE="%ProgramFiles(X86)%\MSBuild\12.0\bin\msbuild.exe"

REM set msbuildemitsolution=1

%MSBUILDEXE% build.proj /m:1 /v:m /t:CleanSolutions

pause