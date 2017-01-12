using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Search.Parameter;
using System;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class QueryFieldParameterTests
    {
        /// <summary>
        /// Where   Using a QueryFieldParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void QueryFieldParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              params:{
                qf:""id^10 score~2^20""
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new QueryFieldParameter<TestDocument>();
            parameter.Configure("id^10 score~2^20");

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a QueryFieldParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void QueryFieldParameter002()
        {
            // Arrange
            var parameter = new QueryFieldParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }
    }
}
