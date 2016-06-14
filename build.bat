@echo off

REM LOCAL VARIABLES
SETLOCAL
SET OUTPUT_DIRECTORY=.

:pack
REM DELETE OLD PACKAGES
cd .
del solrexpress.*.nupkg

REM BUILD SIGNED PACKAGES
dotnet pack src/SolrExpress.Core/project.json --configuration release --output .
dotnet pack src/SolrExpress.Solr4/project.json --configuration release --output .
dotnet pack src/SolrExpress.Solr5/project.json --configuration release --output .

@echo.

pause