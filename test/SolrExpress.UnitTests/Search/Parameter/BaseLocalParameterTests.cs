using Moq;
using SolrExpress.Search.Parameter;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseLocalParameterTests
    {
        /// <summary>
        /// Where   Using a BaseLocalParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseLocalParameter001()
        {
            // Arrange
            var parameter1 = new Mock<BaseLocalParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Name = "A";

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseLocalParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseLocalParameter002()
        {
            // Arrange
            var parameter1 = new Mock<BaseLocalParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Name = "A";

            var parameter2 = new Mock<BaseLocalParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter2.Name = "B";

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseLocalParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseLocalParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseLocalParameter<TestDocument>>
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
