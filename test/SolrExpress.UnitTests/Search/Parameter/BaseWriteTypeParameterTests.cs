using Moq;
using SolrExpress.Search.Parameter;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseWriteTypeParameterTests
    {
        /// <summary>
        /// Where   Using a BaseWriteTypeParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseWriteTypeParameter001()
        {
            // Arrange
            var parameter1 = new Mock<BaseWriteTypeParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Value = WriteType.Json;

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseWriteTypeParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseWriteTypeParameter002()
        {
            // Arrange
            var parameter1 = new Mock<BaseWriteTypeParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Value = WriteType.Json;

            var parameter2 = new Mock<BaseWriteTypeParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter2.Value = WriteType.Smile;

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseWriteTypeParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseWriteTypeParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseWriteTypeParameter<TestDocument>>
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
