using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Solr5.Search.Parameter;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class QueryFieldParameterTests
    {
        /// <summary>
        /// Where   Using a QueryFieldParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void QueryFieldParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              params:{
                qf:""id^10 score~2^20""
              }
            }");
            var container = new JObject();
            var parameter = (IQueryFieldParameter<TestDocument>)new QueryFieldParameter<TestDocument>();
            parameter.Expression("id^10 score~2^20");

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
