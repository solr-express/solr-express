using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Utility
{
    /// <summary>
    /// Helper class used in cache warmup
    /// </summary>
    internal class ExpressionCacheWarmup<TDocument>
        where TDocument : IDocument
    {
        private ExpressionUtility _expressionUtility;

        internal ExpressionCacheWarmup(ExpressionUtility expressionUtility)
        {
            this._expressionUtility = expressionUtility;
        }

        internal void Load()
        {
            var documentParameter = Expression.Parameter(typeof(TDocument), "document");
            var properties = typeof(TDocument).GetProperties();

            foreach (var propertye in properties)
            {
                var nameProperty = Expression.Convert(Expression.Property(documentParameter, propertye.Name), typeof(object));

                var expression = Expression.Lambda<Func<TDocument, object>>(nameProperty, documentParameter);

                this._expressionUtility.GetFieldNameFromExpression(expression);
                this._expressionUtility.GetSolrFieldAttributeFromPropertyInfo(expression);
            }
        }
    }
}
