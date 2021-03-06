﻿using SolrExpress.Search.Query;
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
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Name of alias added in query</param>
        public static IFacetSpatialParameter<TDocument> AliasName<TDocument>(this IFacetSpatialParameter<TDocument> parameter, string value)
            where TDocument : Document
        {
            parameter.AliasName = value;

            return parameter;
        }

        /// <summary>
        /// Configure function used in spatial filter
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Function used in spatial filter</param>
        public static IFacetSpatialParameter<TDocument> FunctionType<TDocument>(this IFacetSpatialParameter<TDocument> parameter, SpatialFunctionType value)
            where TDocument : Document
        {
            parameter.FunctionType = value;

            return parameter;
        }

        /// <summary>
        /// Configure expression used to find field name
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Expression used to find field name</param>
        public static IFacetSpatialParameter<TDocument> FieldExpression<TDocument>(this IFacetSpatialParameter<TDocument> parameter, Expression<Func<TDocument, object>> value)
            where TDocument : Document
        {
            parameter.FieldExpression = value;

            return parameter;
        }

        /// <summary>
        /// Configure center point to spatial filter
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Center point to spatial filter</param>
        public static IFacetSpatialParameter<TDocument> CenterPoint<TDocument>(this IFacetSpatialParameter<TDocument> parameter, GeoCoordinate value)
            where TDocument : Document
        {
            parameter.CenterPoint = value;

            return parameter;
        }

        /// <summary>
        /// Configure distance from center point
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Distance from center point</param>
        public static IFacetSpatialParameter<TDocument> Distance<TDocument>(this IFacetSpatialParameter<TDocument> parameter, decimal value)
            where TDocument : Document
        {
            parameter.Distance = value;

            return parameter;
        }

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Sort type of result of facet</param>
        public static IFacetSpatialParameter<TDocument> SortType<TDocument>(this IFacetSpatialParameter<TDocument> parameter, FacetSortType value)
            where TDocument : Document
        {
            parameter.SortType = value;

            return parameter;
        }

        /// <summary>
        /// Configure list of tags to exclude in facet calculation
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="values">List of tags to exclude in facet calculation</param>
        public static IFacetSpatialParameter<TDocument> Excludes<TDocument>(this IFacetSpatialParameter<TDocument> parameter, params string[] values)
            where TDocument : Document
        {
            parameter.Excludes = values;

            return parameter;
        }

        /// <summary>
        /// Configure filter or list of filters to be intersected with the incoming domain before faceting
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IFacetSpatialParameter<TDocument> Filter<TDocument>(this IFacetSpatialParameter<TDocument> parameter, SearchQuery<TDocument> value)
            where TDocument : Document
        {
            parameter.Filter = value;

            return parameter;
        }
    }
}
