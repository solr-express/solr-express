using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr5.Search.Parameter;
using SolrExpress.Utility;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class DefaultFieldParameterTests
    {
        /// <summary>
        /// Where   Using a DefaultFieldParameter instance
        /// When    Invoking method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void DefaultFieldParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""params"":
                {
                    ""df"": ""id""
                }
            }");
            var container = new JObject();
            var parameter = (IDefaultFieldParameter<TestDocument>)new DefaultFieldParameter<TestDocument>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            parameter.ExpressionBuilder = expressionBuilder;
            parameter.FieldExpression = (q) => q.Id;

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
