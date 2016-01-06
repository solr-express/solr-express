@echo off

REM LOCAL VARIABLES
SETLOCAL
SET NUGETAPP=.nuget\nuget.exe

REM CHECK NUGET.EXE, DOWNLOAD IF NECESSARY
IF EXIST %NUGETAPP% goto updatenuget
@echo.
IF NOT EXIST .nuget md .nuget
@powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe' -OutFile '%NUGETAPP%'"
goto pack

:updatenuget
@echo.
REM %NUGETAPP% update -self

:pack
REM BUILD SIGNED PACKAGES
.nuget\nuget.exe pack SolrExpress.Core\SolrExpress.Core.csproj -IncludeReferencedProjects -Properties Configuration=Release -Properties DefineConstants=STRONGNAME -Verbosity detailed -build
.nuget\nuget.exe pack SolrExpress.Solr4\SolrExpress.Solr4.csproj -IncludeReferencedProjects -Properties Configuration=Release -Properties DefineConstants=STRONGNAME -Verbosity detailed -build
.nuget\nuget.exe pack SolrExpress.Solr5\SolrExpress.Solr5.csproj -IncludeReferencedProjects -Properties Configuration=Release -Properties DefineConstants=STRONGNAME -Verbosity detailed -build

REM BUILD NOT SIGNED PACKAGES
.nuget\nuget.exe pack SolrExpress.Core\SolrExpress.Core.csproj -IncludeReferencedProjects -Properties Configuration=Release -Verbosity detailed -build
.nuget\nuget.exe pack SolrExpress.Solr4\SolrExpress.Solr4.csproj -IncludeReferencedProjects -Properties Configuration=Release -Verbosity detailed -build
.nuget\nuget.exe pack SolrExpress.Solr5\SolrExpress.Solr5.csproj -IncludeReferencedProjects -Properties Configuration=Release -Verbosity detailed -build
@echo.

pause