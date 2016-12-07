using SolrExpress.Core.Utility;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace SolrExpress.Core.UnitTests.Utility
{
    public class ExpressionCacheTests
    {
        /// <summary>
        /// Where   Using ExpressionCache class
        /// When    Invoking the method "Get"
        /// What    Return false and output null values
        /// </summary>
        [Fact]
        public void ExpressionCache001()
        {
            // Arrange
            bool result;
            PropertyInfo propertyInfo;
            SolrFieldAttribute solrFieldAttribute;

            var expressionCache = (IExpressionCache<TestDocument>)new ExpressionCache<TestDocument>();
            Expression<Func<TestDocument, object>> expression = q => q.Dummy;

            // Act
            result = expressionCache.Get(expression, out propertyInfo, out solrFieldAttribute);

            // Assert
            Assert.False(result);
            Assert.Null(propertyInfo);
            Assert.Null(solrFieldAttribute);
        }

        /// <summary>
        /// Where   Using ExpressionCache class
        /// When    Invoking the method "Get" after a invoking "Set"
        /// What    Return true and output same values informed in "Set" method
        /// </summary>
        [Fact]
        public void ExpressionCache002()
        {
            // Arrange
            bool result;
            PropertyInfo propertyInfoFromGet;
            SolrFieldAttribute solrFieldAttributeFromGet;

#if NETCOREAPP1_0
            var propertyInfoOriginal = typeof(TestDocumentWithAttribute).GetTypeInfo().GetProperty("Stored");
#else
            var propertyInfoOriginal = typeof(TestDocumentWithAttribute).GetProperty("Stored");
#endif

            var solrFieldAttributeOriginal = (SolrFieldAttribute)propertyInfoOriginal.GetCustomAttributes(true).FirstOrDefault(q => q is SolrFieldAttribute);

            var expressionCache = (IExpressionCache<TestDocumentWithAttribute>)new ExpressionCache<TestDocumentWithAttribute>();
            Expression<Func<TestDocumentWithAttribute, object>> expression = q => q.Stored;
            expressionCache.Set(expression, propertyInfoOriginal, solrFieldAttributeOriginal);

            // Act
            result = expressionCache.Get(expression, out propertyInfoFromGet, out solrFieldAttributeFromGet);

            // Assert
            Assert.True(result);
            Assert.NotNull(propertyInfoFromGet);
            Assert.NotNull(solrFieldAttributeFromGet);
            Assert.Same(propertyInfoOriginal, propertyInfoFromGet);
            Assert.Same(solrFieldAttributeOriginal, solrFieldAttributeFromGet);
        }

        /// <summary>
        /// Where   Using ExpressionCache class with expressions exact equals
        /// When    Invoking the method "Get" (with expression1) and "Get" (with expression2) (both after a invoking "Set")
        /// What    Return true and output same values informed in "Set" method
        /// </summary>
        [Fact]
        public void ExpressionCache003()
        {
            // Arrange
            bool result1;
            bool result2;
            PropertyInfo propertyInfoFromGet1;
            SolrFieldAttribute solrFieldAttributeFromGet1;
            PropertyInfo propertyInfoFromGet2;
            SolrFieldAttribute solrFieldAttributeFromGet2;
#if NETCOREAPP1_0
            var propertyInfoOriginal = typeof(TestDocumentWithAttribute).GetTypeInfo().GetProperty("Stored");
#else
            var propertyInfoOriginal = typeof(TestDocumentWithAttribute).GetProperty("Stored");
#endif

            var solrFieldAttributeOriginal = (SolrFieldAttribute)propertyInfoOriginal.GetCustomAttributes(true).FirstOrDefault(q => q is SolrFieldAttribute);

            var expressionCache = (IExpressionCache<TestDocumentWithAttribute>)new ExpressionCache<TestDocumentWithAttribute>();
            Expression<Func<TestDocumentWithAttribute, object>> expression = q => q.Stored;
            Expression<Func<TestDocumentWithAttribute, object>> expression1 = q => q.Stored;
            Expression<Func<TestDocumentWithAttribute, object>> expression2 = q => q.Stored;
            expressionCache.Set(expression1, propertyInfoOriginal, solrFieldAttributeOriginal);

            // Act
            result1 = expressionCache.Get(expression1, out propertyInfoFromGet1, out solrFieldAttributeFromGet1);
            result2 = expressionCache.Get(expression2, out propertyInfoFromGet2, out solrFieldAttributeFromGet2);

            // Assert
            Assert.True(result1);
            Assert.NotNull(propertyInfoFromGet1);
            Assert.NotNull(solrFieldAttributeFromGet1);
            Assert.Same(propertyInfoOriginal, propertyInfoFromGet1);
            Assert.Same(solrFieldAttributeOriginal, solrFieldAttributeFromGet1);

            Assert.True(result2);
            Assert.NotNull(propertyInfoFromGet2);
            Assert.NotNull(solrFieldAttributeFromGet2);
            Assert.Same(propertyInfoOriginal, propertyInfoFromGet2);
            Assert.Same(solrFieldAttributeOriginal, solrFieldAttributeFromGet2);
        }

        /// <summary>
        /// Where   Using ExpressionCache class with expressions using same property
        /// When    Invoking the method "Get" (with expression1) and "Get" (with expression2) (both after a invoking "Set")
        /// What    Return true and output same values informed in "Set" method
        /// </summary>
        [Fact]
        public void ExpressionCache004()
        {
            // Arrange
            bool result1;
            bool result2;
            PropertyInfo propertyInfoFromGet1;
            SolrFieldAttribute solrFieldAttributeFromGet1;
            PropertyInfo propertyInfoFromGet2;
            SolrFieldAttribute solrFieldAttributeFromGet2;
#if NETCOREAPP1_0
            var propertyInfoOriginal = typeof(TestDocumentWithAttribute).GetTypeInfo().GetProperty("Stored");
#else
            var propertyInfoOriginal = typeof(TestDocumentWithAttribute).GetProperty("Stored");
#endif

            var solrFieldAttributeOriginal = (SolrFieldAttribute)propertyInfoOriginal.GetCustomAttributes(true).FirstOrDefault(q => q is SolrFieldAttribute);

            var expressionCache = (IExpressionCache<TestDocumentWithAttribute>)new ExpressionCache<TestDocumentWithAttribute>();
            Expression<Func<TestDocumentWithAttribute, object>> expression = a => a.Stored;
            Expression<Func<TestDocumentWithAttribute, object>> expression1 = b => b.Stored;
            Expression<Func<TestDocumentWithAttribute, object>> expression2 = c => c.Stored;
            expressionCache.Set(expression1, propertyInfoOriginal, solrFieldAttributeOriginal);

            // Act
            result1 = expressionCache.Get(expression1, out propertyInfoFromGet1, out solrFieldAttributeFromGet1);
            result2 = expressionCache.Get(expression2, out propertyInfoFromGet2, out solrFieldAttributeFromGet2);

            // Assert
            Assert.True(result1);
            Assert.NotNull(propertyInfoFromGet1);
            Assert.NotNull(solrFieldAttributeFromGet1);
            Assert.Same(propertyInfoOriginal, propertyInfoFromGet1);
            Assert.Same(solrFieldAttributeOriginal, solrFieldAttributeFromGet1);

            Assert.True(result2);
            Assert.NotNull(propertyInfoFromGet2);
            Assert.NotNull(solrFieldAttributeFromGet2);
            Assert.Same(propertyInfoOriginal, propertyInfoFromGet2);
            Assert.Same(solrFieldAttributeOriginal, solrFieldAttributeFromGet2);
        }
    }
}
