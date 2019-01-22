using Moq;
using SolrExpress.Search.Parameter;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseAnyParameterTests
    {
        /// <summary>
        /// Where   Using a BaseAnyParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseAnyParameter001()
        {
            // Arrange
            var parameter1 = new Mock<BaseAnyParameter>
            {
                CallBase = true
            }.Object;
            parameter1.Name = "x";
            parameter1.Value = "y";

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseAnyParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseAnyParameter002()
        {
            // Arrange
            var parameter1 = new Mock<BaseAnyParameter>
            {
                CallBase = true
            }.Object;
            parameter1.Name = "x";
            parameter1.Value = "y";

            var parameter2 = new Mock<BaseAnyParameter>
            {
                CallBase = true
            }.Object;
            parameter2.Name = "x2";
            parameter2.Value = "y2";

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseAnyParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseAnyParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseAnyParameter>
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
