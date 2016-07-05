using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Query.Result;

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

            var parameter = new FacetFieldResult<TestDocument>();

            // Act
            parameter.Execute(null, jObject);

            // Assert
            Assert.Equal(1, parameter.Data.Count);
            Assert.Equal("facetField", parameter.Data[0].Name);
            Assert.Equal(2, parameter.Data[0].Data.Count);
            Assert.True(parameter.Data[0].Data.ContainsKey("VALUE001"));
            Assert.Equal(10, parameter.Data[0].Data["VALUE001"]);
            Assert.True(parameter.Data[0].Data.ContainsKey("VALUE002"));
            Assert.Equal(20, parameter.Data[0].Data["VALUE002"]);
        }
    }
}
