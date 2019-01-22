using Moq;
using SolrExpress.Builder;
using SolrExpress.Options;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseBoostParameterTests
    {
        /// <summary>
        /// Where   Using a BaseBoostParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseBoostParameter001()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);

            var parameter1 = new Mock<BaseBoostParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.BoostFunctionType = BoostFunctionType.Bf;
            parameter1.Query = searchQuery.Field(q => q.Id);

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseBoostParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseBoostParameter002()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);

            var parameter1 = new Mock<BaseBoostParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.BoostFunctionType = BoostFunctionType.Bf;
            parameter1.Query = searchQuery.Field(q => q.Id);

            var parameter2 = new Mock<BaseBoostParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter2.BoostFunctionType = BoostFunctionType.Boost;
            parameter2.Query = searchQuery.Field(q => q.Id);

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseBoostParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseBoostParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseBoostParameter<TestDocument>>
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
