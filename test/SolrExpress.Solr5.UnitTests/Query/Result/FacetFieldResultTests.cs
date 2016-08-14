using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr5.Query.Result;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Query.Result
{
    public class FacetFieldResultTests
    {
        /// <summary>
        /// Where   Using a FacetFieldResult instance
        /// When    Invoking the method "Execute" using a valid JSON
        /// What    Parse to informed concret classes
        /// </summary>
        [Fact]
        public void FacetFieldResult001()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                ""facets"": {
                    ""count"": 100,
                    ""facetField"": {
                        ""buckets"": [
                        {
                            ""val"": ""VALUE001"",
                            ""count"": 10
                        },
                        {
                            ""val"": ""VALUE002"",
                            ""count"": 20
                        }]
                    }
                }
            }");

            var parameters = new List<IParameter>();
            var result = new FacetFieldResult<TestDocument>();

            // Act
            result.Execute(parameters, jObject);

            // Assert
            Assert.Equal(1, result.Data.Count);
            Assert.Equal("facetField", result.Data[0].Name);
            Assert.Equal(2, result.Data[0].Data.Count);
            Assert.True(result.Data[0].Data.ContainsKey("VALUE001"));
            Assert.Equal(10, result.Data[0].Data["VALUE001"]);
            Assert.True(result.Data[0].Data.ContainsKey("VALUE002"));
            Assert.Equal(20, result.Data[0].Data["VALUE002"]);
        }
    }
}
