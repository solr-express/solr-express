using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Core.Utility
{
    /// <summary>
    /// Helper class used in cache warmup
    /// </summary>
    internal static class ExpressionCacheWarmup
    {
        internal static void Load<TDocument>(IExpressionBuilder<TDocument> expressionBuilder)
            where TDocument : IDocument
        {
            var documentParameter = Expression.Parameter(typeof(TDocument), "document");
#if NETCOREAPP1_0
            var properties = typeof(TDocument).GetTypeInfo().GetProperties();
#else
            var properties = typeof(TDocument).GetProperties();
#endif

            foreach (var propertye in properties)
            {
                var nameProperty = Expression.Convert(Expression.Property(documentParameter, propertye.Name), typeof(object));

                var expression = Expression.Lambda<Func<TDocument, object>>(nameProperty, documentParameter);

                expressionBuilder.GetFieldNameFromExpression(expression);
            }
        }
    }
}
