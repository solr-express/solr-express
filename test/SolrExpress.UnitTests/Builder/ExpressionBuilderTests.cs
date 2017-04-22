using SolrExpress.Builder;
using Xunit;

namespace SolrExpress.UnitTests.Builder
{
    public class ExpressionBuilderTests
    {
        /// <summary>
        /// Where   Using a ExpressionBuilder class
        /// When    Invoke GetFieldName with a field without IsDynamic settings
        /// What    Returns original field name
        /// </summary>
        [Fact]
        public void ExpressionBuilder001()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions();
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions);
            expressionBuilder.LoadDocument();

            // Act
            var fieldName = expressionBuilder.GetFieldName(q => q.NoDynamic);

            // Assert
            Assert.Equal("no_dynamic", fieldName);
        }

        /// <summary>
        /// Where   Using a ExpressionBuilder class
        /// When    Invoke GetFieldName with a field with dynamic prefix and suffix settings
        /// What    Returns field name with prefix and suffix
        /// </summary>
        [Fact]
        public void ExpressionBuilder002()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions();
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions);
            expressionBuilder.LoadDocument();

            // Act
            var fieldName = expressionBuilder.GetFieldName(q => q.DynamicWithPrefixAndSufix);

            // Assert
            Assert.Equal("prefix_dynamic_suffix", fieldName);
        }

        /// <summary>
        /// Where   Using a ExpressionBuilder class
        /// When    Invoke GetFieldName with a field with dynamic prefix settings
        /// What    Returns field name with prefix and suffix
        /// </summary>
        [Fact]
        public void ExpressionBuilder003()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions();
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions);
            expressionBuilder.LoadDocument();

            // Act
            var fieldName = expressionBuilder.GetFieldName(q => q.DynamicWithPrefix);

            // Assert
            Assert.Equal("prefix_dynamic", fieldName);
        }

        /// <summary>
        /// Where   Using a ExpressionBuilder class
        /// When    Invoke GetFieldName with a field with dynamic suffix settings
        /// What    Returns field name with prefix and suffix
        /// </summary>
        [Fact]
        public void ExpressionBuilder004()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions();
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions);
            expressionBuilder.LoadDocument();

            // Act
            var fieldName = expressionBuilder.GetFieldName(q => q.DynamicWithSuffix);

            // Assert
            Assert.Equal("dynamic_suffix", fieldName);
        }
    }
}
