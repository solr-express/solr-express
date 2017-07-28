using SolrExpress.Core.Search.Parameter;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure in facet spatial parameter
    /// </summary>
    public static class IFacetSpatialParameterExtension

    {
        /// <summary>
        /// Configure name of alias added in query
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="aliasName">Name of alias added in query</param>
        public static IFacetSpatialParameter<TDocument> AliasName<TDocument>(this IFacetSpatialParameter<TDocument> parameter, string aliasName)
            where TDocument : Document
        {
            parameter.AliasName = aliasName;

            return parameter;
        }

        /// <summary>
        /// Configure function used in spatial filter
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="functionType">Function used in spatial filter</param>
        public static IFacetSpatialParameter<TDocument> FunctionType<TDocument>(this IFacetSpatialParameter<TDocument> parameter, SpatialFunctionType functionType)
            where TDocument : Document
        {
            parameter.FunctionType = functionType;

            return parameter;
        }

        /// <summary>
        /// Configure expression used to find field name
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        public static IFacetSpatialParameter<TDocument> FieldExpression<TDocument>(this IFacetSpatialParameter<TDocument> parameter, Expression<Func<TDocument, object>> fieldExpression)
            where TDocument : Document
        {
            parameter.FieldExpression = fieldExpression;

            return parameter;
        }

        /// <summary>
        /// Configure center point to spatial filter
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        public static IFacetSpatialParameter<TDocument> CenterPoint<TDocument>(this IFacetSpatialParameter<TDocument> parameter, GeoCoordinate centerPoint)
            where TDocument : Document
        {
            parameter.CenterPoint = centerPoint;

            return parameter;
        }

        /// <summary>
        /// Configure distance from center point
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="distance">Distance from center point</param>
        public static IFacetSpatialParameter<TDocument> Distance<TDocument>(this IFacetSpatialParameter<TDocument> parameter, decimal distance)
            where TDocument : Document
        {
            parameter.Distance = distance;

            return parameter;
        }

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="sortType">Sort type of result of facet</param>
        public static IFacetSpatialParameter<TDocument> SortType<TDocument>(this IFacetSpatialParameter<TDocument> parameter, FacetSortType sortType)
            where TDocument : Document
        {
            parameter.SortType = sortType;

            return parameter;
        }

        /// <summary>
        /// Configure list of tags to exclude in facet calculation
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static IFacetSpatialParameter<TDocument> Excludes<TDocument>(this IFacetSpatialParameter<TDocument> parameter, params string[] excludes)
            where TDocument : Document
        {
            parameter.Excludes = excludes;

            return parameter;
        }
    }
}
