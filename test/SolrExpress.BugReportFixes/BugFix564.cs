using Microsoft.Extensions.DependencyInjection;
using SolrExpress.DI.CoreClr;
using SolrExpress.Search.Extension;
using SolrExpress.Search.Result;
using SolrExpress.Search.Result.Extension;
using SolrExpress.Solr5.Extension;
using Xunit;

namespace SolrExpress.BugReportFixes
{
    /// <summary>
    /// NullReferenceException in ValidateSearchItem() > CheckAnyParameter
    /// Related by: Akaoni
    /// https://github.com/solr-express/solr-express/issues/564
    /// </summary>
    public class BugFix564
    {
        [Fact]
        public void BugFixTest()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddSolrExpress<Document>(builder => builder
                    .UseOptions(options => options.HostAddress = "http://localhost:8983/solr/techproducts")
                    .UseSolr5())
                .BuildServiceProvider();

            var search = serviceProvider.GetService<DocumentCollection<Document>>()
                .Select()
                .Limit(0);

            // Act
            search
                .Execute()
                .Information(out Information information);

            // Assert
            Assert.Equal(0, information.DocumentCount);
        }
    }
}
