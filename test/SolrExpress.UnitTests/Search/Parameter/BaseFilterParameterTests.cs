using Moq;
using SolrExpress.Search.Parameter;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseFilterParameterTests
    {
        /// <summary>
        /// Where   Using a BaseFilterParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseFilterParameter001()
        {
            // Arrange
            var parameter1 = new Mock<BaseFilterParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.TagName = "A";

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseFilterParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseFilterParameter002()
        {
            // Arrange
            var parameter1 = new Mock<BaseFilterParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.TagName = "A";

            var parameter2 = new Mock<BaseFilterParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter2.TagName = "B";

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseFilterParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseFilterParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseFilterParameter<TestDocument>>
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
