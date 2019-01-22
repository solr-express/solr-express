using Moq;
using SolrExpress.Search.Parameter;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseLimitParameterTests
    {
        /// <summary>
        /// Where   Using a BaseLimitParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseLimitParameter001()
        {
            // Arrange
            var parameter1 = new Mock<BaseLimitParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Value = 1;

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseLimitParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseLimitParameter002()
        {
            // Arrange
            var parameter1 = new Mock<BaseLimitParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Value = 1;

            var parameter2 = new Mock<BaseLimitParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter2.Value = 2;

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseLimitParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseLimitParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseLimitParameter<TestDocument>>
            {
                CallBase = true
            }.Object;

            var parameter2 = new FakeSearchParameter();

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }
    }
}
