@echo off

dotnet test ./test/SolrExpress.Core.UnitTests -c Release -f netcoreapp1.0
dotnet build ./test/SolrExpress.Core.UnitTests -c Release -f net451

mono \  
./test/SolrExpress.Core.UnitTests/bin/Release/net451/*/dotnet-test-xunit.exe \
./test/SolrExpress.Core.UnitTests/bin/Release/net451/*/TEST_PROJECT_NAME.dll

dotnet pack ./src/SolrExpress.Core -c Release -o ./artifacts

@echo.

pause