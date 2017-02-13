using SolrExpress.Search.Parameter;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Extension
{
    /// <summary>
    /// Extensions to configure in sort parameter
    /// </summary>
    public static class ISortParameterExtension
    {
        /// <summary>
        /// Configure expression used to find field name
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="fieldExpressions">Expressions used to find fields names</param>
        public static ISortParameter<TDocument> FieldExpressions<TDocument>(this ISortParameter<TDocument> parameter, Expression<Func<TDocument, object>>[] fieldExpressions)
            where TDocument : IDocument
        {
            parameter.FieldExpressions = fieldExpressions;

            return parameter;
        }

        /// <summary>
        /// Configure true to ascendent order, otherwise false
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public static ISortParameter<TDocument> Ascendent<TDocument>(this ISortParameter<TDocument> parameter, bool ascendent)
            where TDocument : IDocument
        {
            parameter.Ascendent = ascendent;

            return parameter;
        }
    }
}
