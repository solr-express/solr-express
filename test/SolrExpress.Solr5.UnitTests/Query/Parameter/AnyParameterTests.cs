using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Query.Parameter;

namespace SolrExpress.Solr5.UnitTests.Query.Parameter
{
    public class AnyParameterTests
    {
        /// <summary>
        /// Where   Using a AnyParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AnyParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""params"":{
                    ""x"": ""y""
                }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new AnyParameter();
            parameter.Configure("x", "y");

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }
    }
}
