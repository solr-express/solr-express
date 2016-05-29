using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Query.Parameter;
using System;

namespace SolrExpress.Solr5.UnitTests.Query.Parameter
{
    public class SortParameterTests
    {
        /// <summary>
        /// Where   Using a SortParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void SortParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""sort"": ""Id asc""
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new SortParameter<TestDocument>();
            parameter.Configure(q => q.Id, true);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a SortParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SortParameter002()
        {
            // Arrange
            var parameter = new SortParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null, true));
        }
    }
}
