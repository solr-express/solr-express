using Moq;
using SolrExpress.Search.Parameter;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseQueryParserParameterTests
    {
        /// <summary>
        /// Where   Using a BaseQueryParserParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseQueryParserParameter001()
        {
            // Arrange
            var parameter1 = new Mock<BaseQueryParserParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Value = QueryParserType.Dismax;

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseQueryParserParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseQueryParserParameter002()
        {
            // Arrange
            var parameter1 = new Mock<BaseQueryParserParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Value = QueryParserType.Dismax;

            var parameter2 = new Mock<BaseQueryParserParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter2.Value = QueryParserType.Edismax;

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseQueryParserParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseQueryParserParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseQueryParserParameter<TestDocument>>
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
