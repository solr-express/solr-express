using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Solr5.Search.Result;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Result
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

            var parameters = new List<ISearchParameter>();
            var result = (IConvertJsonObject)new FacetQueryResult<TestDocument>();

            // Act
            result.Execute(parameters, jObject);

            // Assert
            var data = ((IFacetQueryResult<TestDocument>)result).Data;
            Assert.Equal(1, data.Count);
            Assert.True(data.ContainsKey("facetQuery"));
            Assert.Equal(10, data["facetQuery"]);
        }
    }
}
