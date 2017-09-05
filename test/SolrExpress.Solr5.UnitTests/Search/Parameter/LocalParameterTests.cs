using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Options;
using SolrExpress.Search;
using SolrExpress.Search.Query;
using SolrExpress.Solr5.Search.Parameter;
using SolrExpress.Utility;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class LocalParameterTests
    {
        /// <summary>
        /// Where   Using a LocalParameter instance
        /// When    Invoking method "Execute"
        /// What    Create correct SOLR instructions
        /// </summary>
        [Fact]
        public void LocalParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""params"": {
                ""myLocalParameter"": ""id:\""ITEM01\""""
              }
            }");
            var container = new JObject();
            var parameter = new LocalParameter<TestDocument>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);
            parameter.Name = "myLocalParameter";
            parameter.Query = searchQuery.Field(q => q.Id).EqualsTo("ITEM01");

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }

        /// <summary>
        /// Where   Using a LocalParameter instance
        /// When    Invoking method "Execute"
        /// What    Create correct SOLR instructions
        /// </summary>
        [Fact]
        public void LocalParameter002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""params"":{
                    ""myLocalParameter"": ""some value""
                }
            }");
            var container = new JObject();
            var parameter = new LocalParameter<TestDocument>();
            parameter.Name = "myLocalParameter";
            parameter.Value = "some value";

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
