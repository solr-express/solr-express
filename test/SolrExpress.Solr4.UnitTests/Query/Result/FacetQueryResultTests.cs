using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr4.Query.Result;
using System.Collections.Generic;
using Xunit;

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

            var parameters = new List<IParameter>();
            var result = new FacetQueryResult<TestDocument>();

            // Act
            result.Execute(parameters, jObject);

            // Assert
            Assert.Equal(1, result.Data.Count);
            Assert.True(result.Data.ContainsKey("facetQuery"));
            Assert.Equal(10, result.Data["facetQuery"]);
        }
    }
}
