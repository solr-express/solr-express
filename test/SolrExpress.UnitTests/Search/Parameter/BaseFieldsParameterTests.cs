using Moq;
using SolrExpress.Builder;
using SolrExpress.Options;
using SolrExpress.Search.Parameter;
using SolrExpress.Utility;
using System;
using System.Linq.Expressions;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseFieldsParameterTests
    {
        /// <summary>
        /// Where   Using a BaseFieldsParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseFieldsParameter001()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var parameter1 = new Mock<BaseFieldsParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.ExpressionBuilder = expressionBuilder;
            parameter1.FieldExpressions = new Expression<Func<TestDocument, object>>[] {
                q => q.Id,
                q => q.Score
            };

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseFieldsParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseFieldsParameter002()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var parameter1 = new Mock<BaseFieldsParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.ExpressionBuilder = expressionBuilder;
            parameter1.FieldExpressions = new Expression<Func<TestDocument, object>>[] {
                q => q.Id,
                q => q.Score
            };

            var parameter2 = new Mock<BaseFieldsParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter2.ExpressionBuilder = expressionBuilder;
            parameter2.FieldExpressions = new Expression<Func<TestDocument, object>>[] {
                q => q.Id,
                q => q.Dummy
            };

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseFieldsParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseFieldsParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseFieldsParameter<TestDocument>>
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
