using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    public static class FacetFieldParameterExtension
    {
        /// <summary>
        /// Configure field expression of informed parameter
        /// </summary>
        /// <param name="parameter">Parameter to congigure</param>
        /// <param name="fieldExpression">Expression used to find the property name</param>
        public static IFacetFieldParameter<TDocument> FieldExpression<TDocument>(this IFacetFieldParameter<TDocument> parameter, Expression<Func<TDocument, object>> fieldExpression)
            where TDocument : IDocument
        {
            parameter.FieldExpression = fieldExpression;

            return parameter;
        }
        
        /// <summary>
        /// Configure list of tags to exclude in facet calculation of informed parameter
        /// </summary>
        /// <param name="parameter">Parameter to congigure</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static IFacetFieldParameter<TDocument> Excludes<TDocument>(this IFacetFieldParameter<TDocument> parameter, params string[] excludes)
            where TDocument : IDocument
        {
            parameter.Excludes = excludes;

            return parameter;
        }
    }
}
