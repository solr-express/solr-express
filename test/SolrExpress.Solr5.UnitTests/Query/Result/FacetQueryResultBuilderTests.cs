using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Query.Result;

namespace SolrExpress.Solr5.UnitTests.Query.Result
{
    public class FacetQueryResultTests
    {
        /// <summary>
        /// Where   Using a FacetQueryResult instance
        /// When    Invoking the method "Execute" using a valid JSON
        /// What    Parse to informed concret classes
        /// </summary>
        [Fact]
        public void FacetQueryResult001()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                ""facets"": {
                    ""count"": 100,
                    ""facetQuery"": {
                      ""count"": 10
                    }
                }
            }");

            var parameter = new FacetQueryResult<TestDocument>();

            // Act
            parameter.Execute(null, jObject);

            // Assert
            Assert.Equal(1, parameter.Data.Count);
            Assert.True(parameter.Data.ContainsKey("facetQuery"));
            Assert.Equal(10, parameter.Data["facetQuery"]);
        }
    }
}
