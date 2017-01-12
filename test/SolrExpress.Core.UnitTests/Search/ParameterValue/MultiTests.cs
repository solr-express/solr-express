using Xunit;
using SolrExpress.Core.Search.ParameterValue;
using System;

namespace SolrExpress.Core.UnitTests.Search.ParameterValue
{
    public class MultiTests
    {
        /// <summary>
        /// Where   Using a Multi instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void Multi001()
        {
            // Arrange/ Act / Assert
            Assert.Throws<ArgumentNullException>(() => new Multi<TestDocument>(SolrQueryConditionType.And, null));
        }

        /// <summary>
        /// Where   Using a Multi instance
        /// When    Create the instance with 1 query parameter
        /// What    Throws ArgumentException
        /// </summary>
        [Fact]
        public void Multi002()
        {
            // Arrange/ Act / Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Multi<TestDocument>(SolrQueryConditionType.And, new Any<TestDocument>("X")));
        }

        /// <summary>
        /// Where   Using a Multi instance
        /// When    Create the instance with AND condition and 2 query parameters
        /// What    Throws ArgumentException
        /// </summary>
        [Fact]
        public void Multi003()
        {
            // Arrange
            var expected = "(value 1 AND value 2)";
            string actual;
            var parameter = new Multi<TestDocument>(SolrQueryConditionType.And, new Any<TestDocument>("value 1"), new Any<TestDocument>("value 2"));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Multi instance
        /// When    Create the instance with OR condition and 2 query parameters
        /// What    Throws ArgumentException
        /// </summary>
        [Fact]
        public void Multi004()
        {
            // Arrange
            var expected = "(value 1 OR value 2)";
            string actual;
            var parameter = new Multi<TestDocument>(SolrQueryConditionType.Or, new Any<TestDocument>("value 1"), new Any<TestDocument>("value 2"));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
