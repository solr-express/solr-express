using Xunit;
using SolrExpress.Core.Query.ParameterValue;
using System;

namespace SolrExpress.Core.Tests.Query.ParameterValue
{
    public class NegateTests
    {
        /// <summary>
        /// Where   Using a Negate instance
        /// When    Create the instance with a value
        /// What    Get the informed value
        /// </summary>
        [Fact]
        public void Negate001()
        {
            // Arrange
            var expected = "-tst";
            string actual;
            var parameter = new Negate(new Any("tst"));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Negate instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void Negate002()
        {
            // Arrange/ Act / Assert
            Assert.Throws<ArgumentNullException>(() => new Negate(null));
        }
    }
}
