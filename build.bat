@echo off

REM LOCAL VARIABLES
SETLOCAL
SET OUTPUT_DIRECTORY=.

:pack
REM DELETE OLD PACKAGES
cd .
del solrexpress.*.nupkg

REM BUILD SIGNED PACKAGES
dotnet restore
dotnet pack src/SolrExpress.Core/SolrExpress.Core.csproj --configuration release --output .
dotnet pack src/SolrExpress.Solr4/SolrExpress.Solr4.csproj --configuration release --output .
dotnet pack src/SolrExpress.Solr5/SolrExpress.Solr5.csproj --configuration release --output .

@echo.

pause