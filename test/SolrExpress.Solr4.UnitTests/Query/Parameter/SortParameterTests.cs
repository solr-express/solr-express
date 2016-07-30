using SolrExpress.Core;
using SolrExpress.Solr4.Query.Parameter;
using System;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Query.Parameter
{
    public class SortParameterTests
    {
        /// <summary>
        /// Where   Using a SortParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void SortParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new SortParameter<TestDocument>();
            parameter.Configure(q => q.Id, true);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("sort=_id_ asc", container[0]);
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
        /// When    Invoking the method "Validate" using field Indexed=true
        /// What    Valid is true
        /// </summary>
        [Fact]
        public void SortParameter003()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var container = new List<string>();
            var parameter = new SortParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.Indexed, true);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
        }

        /// <summary>
        /// Where   Using a SortParameter instance
        /// When    Invoking the method "Validate" using field Indexed=false
        /// What    Valid is true
        /// </summary>
        [Fact]
        public void SortParameter004()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var container = new List<string>();
            var parameter = new SortParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.NotIndexed, true);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.False(isValid);
            Assert.Equal(Resource.FieldMustBeIndexedTrueToBeUsedInASortException, errorMessage);
        }
    }
}
