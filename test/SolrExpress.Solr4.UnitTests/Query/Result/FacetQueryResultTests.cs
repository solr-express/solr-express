using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr4.Query.Result;

namespace SolrExpress.Solr4.UnitTests.Query.Result
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
                 ""facet_counts"":{
                    ""facet_queries"":{
                      ""facetQuery"":10}
                }
            }");

            var parameter = new FacetQueryResult<TestDocument>();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.Equal(1, parameter.Data.Count);
            Assert.True(parameter.Data.ContainsKey("facetQuery"));
            Assert.Equal(10, parameter.Data["facetQuery"]);
        }
    }
}
