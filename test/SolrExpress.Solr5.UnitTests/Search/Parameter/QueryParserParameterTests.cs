using Newtonsoft.Json.Linq;
using SolrExpress.Options;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr5.Search.Parameter;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
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
            var expected = JObject.Parse(@"
            {
                ""params"":
                {
                    ""defType"": ""dismax""
                }
            }");
            var container = new JObject();
            var parameter = (IQueryParserParameter<TestDocument>)new QueryParserParameter<TestDocument>();
            var solrExpressOptions = new SolrExpressOptions();
            parameter.Value = QueryParserType.Dismax;

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
