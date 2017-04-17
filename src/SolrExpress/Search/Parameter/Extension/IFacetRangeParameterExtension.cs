using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure in facet range parameter
    /// </summary>
    public static class IFacetRangeParameterExtension
    {
        /// <summary>
        /// Configure name of alias added in query
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="aliasName"Name of alias added in query></param>
        public static IFacetRangeParameter<TDocument> AliasName<TDocument>(this IFacetRangeParameter<TDocument> parameter, string aliasName)
            where TDocument : IDocument
        {
            parameter.AliasName = aliasName;

            return parameter;
        }

        /// <summary>
        /// Configure expression used to find field name
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        public static IFacetRangeParameter<TDocument> FieldExpression<TDocument>(this IFacetRangeParameter<TDocument> parameter, Expression<Func<TDocument, object>> fieldExpression)
            where TDocument : IDocument
        {
            parameter.FieldExpression = fieldExpression;

            return parameter;
        }

        /// <summary>
        /// Configure size of each range bucket to make facet
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="gap">Size of each range bucket to make facet</param>
        public static IFacetRangeParameter<TDocument> Gap<TDocument>(this IFacetRangeParameter<TDocument> parameter, string gap)
            where TDocument : IDocument
        {
            parameter.Gap = gap;

            return parameter;
        }

        /// <summary>
        /// Configure lower bound to make facet
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="start">Lower bound to make facet</param>
        public static IFacetRangeParameter<TDocument> Start<TDocument>(this IFacetRangeParameter<TDocument> parameter, string start)
            where TDocument : IDocument
        {
            parameter.Start = start;

            return parameter;
        }

        /// <summary>
        /// Configure upper bound to make facet
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="end">Upper bound to make facet</param>
        public static IFacetRangeParameter<TDocument> End<TDocument>(this IFacetRangeParameter<TDocument> parameter, string end)
            where TDocument : IDocument
        {
            parameter.End = end;

            return parameter;
        }

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="sortType">Sort type of result of facet</param>
        public static IFacetRangeParameter<TDocument> SortType<TDocument>(this IFacetRangeParameter<TDocument> parameter, FacetSortType sortType)
            where TDocument : IDocument
        {
            parameter.SortType = sortType;

            return parameter;
        }

        /// <summary>
        /// Configure list of tags to exclude in facet calculation
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static IFacetRangeParameter<TDocument> Excludes<TDocument>(this IFacetRangeParameter<TDocument> parameter, params string[] excludes)
            where TDocument : IDocument
        {
            parameter.Excludes = excludes;

            return parameter;
        }

        /// <summary>
        /// Configure counts should also be computed for all records with field values lower then lower bound of the first range
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="countBefore">Counts should also be computed for all records with field values lower then lower bound of the first range</param>
        public static IFacetRangeParameter<TDocument> CountBefore<TDocument>(this IFacetRangeParameter<TDocument> parameter, bool countBefore)
            where TDocument : IDocument
        {
            parameter.CountBefore = countBefore;

            return parameter;
        }

        /// <summary>
        /// Configure counts should also be computed for all records with field values greater then the upper bound of the last range
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="countAfter">Counts should also be computed for all records with field values greater then the upper bound of the last range</param>
        public static IFacetRangeParameter<TDocument> CountAfter<TDocument>(this IFacetRangeParameter<TDocument> parameter, bool countAfter)
            where TDocument : IDocument
        {
            parameter.CountAfter = countAfter;

            return parameter;
        }
    }
}