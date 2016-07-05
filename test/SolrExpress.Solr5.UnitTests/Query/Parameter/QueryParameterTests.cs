using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr5.Query.Parameter;
using System;

namespace SolrExpress.Solr5.UnitTests.Query.Parameter
{
    public class QueryParameterTests
    {
        /// <summary>
        /// Where   Using a QueryParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void QueryParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""query"": ""Id:ITEM01""
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new QueryParameter<TestDocument>();
            parameter.Configure(new Single<TestDocument>(q => q.Id, "ITEM01"));

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a QueryParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void QueryParameter002()
        {
            // Arrange
            var parameter = new QueryParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }
    }
}
