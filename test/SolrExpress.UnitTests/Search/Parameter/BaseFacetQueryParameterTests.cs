using Moq;
using SolrExpress.Search.Parameter;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseFacetQueryParameterTests
    {
        /// <summary>
        /// Where   Using a BaseFacetQueryParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseFacetQueryParameter001()
        {
            // Arrange
            var parameter1 = new Mock<BaseFacetQueryParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.AliasName = "a";

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseFacetQueryParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseFacetQueryParameter002()
        {
            // Arrange
            var parameter1 = new Mock<BaseFacetQueryParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.AliasName = "A";

            var parameter2 = new Mock<BaseFacetQueryParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter2.AliasName = "B";

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseFacetQueryParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseFacetQueryParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseFacetQueryParameter<TestDocument>>
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
