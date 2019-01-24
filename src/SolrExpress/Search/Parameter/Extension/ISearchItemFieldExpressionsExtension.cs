using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure FieldExpressions
    /// </summary>
    public static class ISearchItemFieldExpressionsExtension

    {
        /// <summary>
        /// Configure expressions used to find fields name
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Expressions used to find fields name</param>
        public static ISearchItemFieldExpressions<TDocument> FieldExpressions<TDocument>(this ISearchItemFieldExpressions<TDocument> parameter, params Expression<Func<TDocument, object>>[] value)
            where TDocument : Document
        {
            parameter.FieldExpressions = value;

            return parameter;
        }
    }
}
