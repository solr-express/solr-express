using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.ParameterValue;
using SolrExpress.Solr5.Search.Parameter;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class BoostParameterTests
    {
        /// <summary>
        /// Where   Using a BoostParameter instance
        /// When    Invoking the method "Execute" using BF function
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void BoostParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""params"": {
                ""bf"": ""id""
                }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new BoostParameter<TestDocument>();
            parameter.Configure(new Any("id"), BoostFunctionType.Bf);
            
            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(actual, expected.ToString());
        }

        /// <summary>
        /// Where   Using a BoostParameter instance
        /// When    Invoking the method "Execute" using Boost function
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void BoostParameter002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""params"": {
                ""boost"": ""id""
                }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new BoostParameter<TestDocument>();
            parameter.Configure(new Any("id"), BoostFunctionType.Boost);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(actual, expected.ToString());
        }
    }
}
