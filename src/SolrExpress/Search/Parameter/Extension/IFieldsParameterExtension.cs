using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Extensions to configure in fields parameter
    /// </summary>
    public static class IFieldsParameterExtension
    {
        /// <summary>
        /// Configure expressions used to find fields name
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="fieldExpressions">Expressions used to find fields name</param>
        public static IFieldsParameter<TDocument> FieldExpressions<TDocument>(this IFieldsParameter<TDocument> parameter, params Expression<Func<TDocument, object>>[] fieldExpressions)
            where TDocument : IDocument
        {
            parameter.FieldExpressions = fieldExpressions;

            return parameter;
        }
    }
}
