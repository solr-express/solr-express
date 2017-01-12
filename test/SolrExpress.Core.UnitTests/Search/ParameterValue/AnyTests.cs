using Xunit;
using SolrExpress.Core.Search.ParameterValue;
using System;

namespace SolrExpress.Core.UnitTests.Search.ParameterValue
{
    public class AnyTests
    {
        /// <summary>
        /// Where   Using a Any instance
        /// When    Create the instance with a value
        /// What    Get the informed value
        /// </summary>
        [Fact]
        public void Any001()
        {
            // Arrange
            var expected = "tst";
            string actual;
            var parameter = new Any<TestDocument>("tst");

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Any instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void Any002()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new Any<TestDocument>(null));
        }
    }
}
