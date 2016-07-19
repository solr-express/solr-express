using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Solr5.Query.Parameter;
using System;
using Xunit;

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
              ""sort"": ""_id_ asc""
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

        /// <summary>
        /// Where   Using a SortParameter instance
        /// When    Invoking the method "Execute" using a expression with a index=false field
        /// What    Validation returns false
        /// </summary>
        [Fact]
        public void SortParameter003()
        {
            // Arrange
            var parameter = new SortParameter<TestDocument>();
            parameter.Configure(q => q.IndexedFalse, true);
            bool isValid;
            string errorMessage;

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.False(isValid);
            Assert.Equal(Resource.FieldMustBeIndexedTrueToBeUsedInASortException, errorMessage);
        }
    }
}
