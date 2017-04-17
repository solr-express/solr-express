using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure facet field parameter
    /// </summary>
    public static class IFacetFieldParameterExtension
    {
        /// <summary>
        /// Configure expression used to find field name
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        public static IFacetFieldParameter<TDocument> FieldExpression<TDocument>(this IFacetFieldParameter<TDocument> parameter, Expression<Func<TDocument, object>> fieldExpression)
            where TDocument : IDocument
        {
            parameter.FieldExpression = fieldExpression;

            return parameter;
        }

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="sortType">Sort type of result of facet</param>
        public static IFacetFieldParameter<TDocument> SortType<TDocument>(this IFacetFieldParameter<TDocument> parameter, FacetSortType sortType)
            where TDocument : IDocument
        {
            parameter.SortType = sortType;

            return parameter;
        }

        /// <summary>
        /// Configure minimum count of itens in facet's result
        /// </summary>
        /// <param name="parameter">Parameter to congigure</param>
        /// <param name="minimum">Minimum count of itens in facet's result</param>
        public static IFacetFieldParameter<TDocument> Minimum<TDocument>(this IFacetFieldParameter<TDocument> parameter, int minimum)
            where TDocument : IDocument
        {
            parameter.Minimum = minimum;

            return parameter;
        }

        /// <summary>
        /// Configure limit of itens in facet's result
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        public static IFacetFieldParameter<TDocument> Limit<TDocument>(this IFacetFieldParameter<TDocument> parameter, int limit)
            where TDocument : IDocument
        {
            parameter.Limit = limit;

            return parameter;
        }

        /// <summary>
        /// Configure list of tags to exclude in facet calculation
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
