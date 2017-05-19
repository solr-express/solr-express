using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr5.Search.Parameter;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class FacetLimitParameterTests
    {
        /// <summary>
        /// Where   Using a FacetLimitParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void FacetLimitParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              params:{
                ""facet.limit"":10
              }
            }");
            var container = new JObject();
            var parameter = (IFacetLimitParameter<TestDocument>)new FacetLimitParameter<TestDocument>();
            parameter.Value = 10;

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
