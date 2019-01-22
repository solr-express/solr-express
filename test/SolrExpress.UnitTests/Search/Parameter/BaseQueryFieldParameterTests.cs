using Moq;
using SolrExpress.Search.Parameter;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseQueryFieldParameterTests
    {
        /// <summary>
        /// Where   Using a BaseQueryFieldParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseQueryFieldParameter001()
        {
            // Arrange
            var parameter1 = new Mock<BaseQueryFieldParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Expression = "A";

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseQueryFieldParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseQueryFieldParameter002()
        {
            // Arrange
            var parameter1 = new Mock<BaseQueryFieldParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Expression = "A";

            var parameter2 = new Mock<BaseQueryFieldParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter2.Expression = "B";

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseQueryFieldParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseQueryFieldParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseQueryFieldParameter<TestDocument>>
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
