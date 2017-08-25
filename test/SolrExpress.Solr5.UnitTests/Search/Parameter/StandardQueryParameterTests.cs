using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Options;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using SolrExpress.Solr5.Search.Parameter;
using SolrExpress.Utility;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class StandardQueryParameterTests
    {
        /// <summary>
        /// Where   Using a StandardQueryParameter instance
        /// When    Invoking method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void StandardQueryParameter001()
        {
            // Arrange
            var expected = JObject.Parse("{\"params\":{\"q.alt\": \"id:\\\"ITEM01\\\"\"}}");
            var container = new JObject();
            var parameter = (IStandardQueryParameter<TestDocument>)new StandardQueryParameter<TestDocument>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);
            parameter.Value = searchQuery.Field(q => q.Id).EqualsTo("ITEM01");

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
