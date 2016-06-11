using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Query.Parameter;

namespace SolrExpress.Solr5.UnitTests.Query.Parameter
{
    public class OffsetParameterTests
    {
        /// <summary>
        /// Where   Using a OffsetParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void OffsetParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""offset"": 10
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new OffsetParameter();
            parameter.Configure(10);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }
    }
}
