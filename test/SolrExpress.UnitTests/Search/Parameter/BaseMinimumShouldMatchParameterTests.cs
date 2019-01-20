using Moq;
using SolrExpress.Search.Parameter;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseMinimumShouldMatchParameterTests
    {
        /// <summary>
        /// Where   Using a BaseMinimumShouldMatchParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseMinimumShouldMatchParameter001()
        {
            // Arrange
            var parameter1 = new Mock<BaseMinimumShouldMatchParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Value = "A";

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseMinimumShouldMatchParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseMinimumShouldMatchParameter002()
        {
            // Arrange
            var parameter1 = new Mock<BaseMinimumShouldMatchParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Value = "A";

            var parameter2 = new Mock<BaseMinimumShouldMatchParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter2.Value = "B";

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseMinimumShouldMatchParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseMinimumShouldMatchParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseMinimumShouldMatchParameter<TestDocument>>
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
