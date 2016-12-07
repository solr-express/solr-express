using SolrExpress.Core.Utility;
using System;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace SolrExpress.Core.UnitTests.Utility
{
    public class ExpressionCacheWarmupTests
    {
        /// <summary>
        /// Where   Using ExpressionCacheWarmup class
        /// When    Invoking the method "Load" 
        /// What    Create expressions in cache of 4 properties of TestDocument class
        /// </summary>
        [Fact]
        public void ExpressionCacheWarmup001()
        {
            // Arrange
            PropertyInfo propertyInfo;
            SolrFieldAttribute solrFieldAttribute;
            var expressionCache = (IExpressionCache<TestDocument>)new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            Expression<Func<TestDocument, object>> expression1 = (q) => q.Id;
            Expression<Func<TestDocument, object>> expression2 = (q) => q.Score;
            Expression<Func<TestDocument, object>> expression3 = (q) => q.Spatial;
            Expression<Func<TestDocument, object>> expression4 = (q) => q.Dummy;
            bool result1;
            bool result2;
            bool result3;
            bool result4;

            // Act
            ExpressionCacheWarmup.Load(expressionBuilder);

            // Assert
            result1 = expressionCache.Get(expression1, out propertyInfo, out solrFieldAttribute);
            result2 = expressionCache.Get(expression3, out propertyInfo, out solrFieldAttribute);
            result3 = expressionCache.Get(expression4, out propertyInfo, out solrFieldAttribute);
            result4 = expressionCache.Get(expression4, out propertyInfo, out solrFieldAttribute);

            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
            Assert.True(result4);
        }
    }
}
