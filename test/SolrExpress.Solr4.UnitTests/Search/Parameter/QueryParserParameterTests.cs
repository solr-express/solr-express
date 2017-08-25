using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using SolrExpress.Options;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class QueryParserParameterTests
    {
        /// <summary>
        /// Where   Using a QueryParserParameter instance
        /// When    Invoking method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void QueryParserParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = (IQueryParserParameter<TestDocument>)new QueryParserParameter<TestDocument>();
            parameter.Value = QueryParserType.Dismax;

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("defType=dismax", container[0]);
        }
    }
}
