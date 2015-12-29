REM .nuget\nuget.exe pack SolrExpress.Solr4\SolrExpress.Solr4.csproj -IncludeReferencedProjects -Properties Configuration=Release -Verbosity detailed -build
REM .nuget\nuget.exe pack SolrExpress.Solr5\SolrExpress.Solr5.csproj -IncludeReferencedProjects -Properties Configuration=Release -Verbosity detailed -build


@echo off

REM LOCAL VARIABLES
SETLOCAL
SET NUGET_VERSION=latest
SET CACHED_NUGET=%LocalAppData%\NuGet\nuget.%NUGET_VERSION%.exe

REM CHECK NUGET.EXE, DOWNLOAD IF NECESSARY
IF EXIST %CACHED_NUGET% goto copynuget
echo Downloading latest version of NuGet.exe...
IF NOT EXIST %LocalAppData%\NuGet md %LocalAppData%\NuGet
@powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest 'https://dist.nuget.org/win-x86-commandline/%NUGET_VERSION%/nuget.exe' -OutFile '%CACHED_NUGET%'"

:copynuget
IF EXIST .nuget\nuget.exe goto buildsigned
md .nuget
copy %CACHED_NUGET% .nuget\nuget.exe > nul

REM BUILD SIGNED PACKAGES
:buildsigned
.nuget\nuget.exe pack SolrExpress.Core\SolrExpress.Core.csproj -IncludeReferencedProjects -Properties Configuration=ReleaseSigned -Verbosity detailed -build
.nuget\nuget.exe pack SolrExpress.Solr4\SolrExpress.Solr4.csproj -IncludeReferencedProjects -Properties Configuration=ReleaseSigned -Verbosity detailed -build
.nuget\nuget.exe pack SolrExpress.Solr5\SolrExpress.Solr5.csproj -IncludeReferencedProjects -Properties Configuration=ReleaseSigned -Verbosity detailed -build

rename SolrExpress.Core.*.nupkg SolrExpress.Core.*.Signed.nupkg
rename SolrExpress.Solr4.*.nupkg SolrExpress.Solr4.*.Signed.nupkg
rename SolrExpress.Solr5.*.nupkg SolrExpress.Solr5.*.Signed.nupkg

REM BUILD NOT SIGNED PACKAGES
.nuget\nuget.exe pack SolrExpress.Core\SolrExpress.Core.csproj -IncludeReferencedProjects -Properties Configuration=Release -Verbosity detailed -build
.nuget\nuget.exe pack SolrExpress.Solr4\SolrExpress.Solr4.csproj -IncludeReferencedProjects -Properties Configuration=Release -Verbosity detailed -build
.nuget\nuget.exe pack SolrExpress.Solr5\SolrExpress.Solr5.csproj -IncludeReferencedProjects -Properties Configuration=Release -Verbosity detailed -build