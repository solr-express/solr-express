using Newtonsoft.Json.Linq;
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
            string actual;
            var jObject = new JObject();
            var parameter = new FacetLimitParameter<TestDocument>();
            parameter.Configure(10);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }
    }
}
