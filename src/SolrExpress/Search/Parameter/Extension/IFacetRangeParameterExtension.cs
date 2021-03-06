﻿using SolrExpress.Search.Query;
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
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="str">Name of alias added in query></param>
        public static IFacetRangeParameter<TDocument> AliasName<TDocument>(this IFacetRangeParameter<TDocument> parameter, string str)
            where TDocument : Document
        {
            parameter.AliasName = str;

            return parameter;
        }

        /// <summary>
        /// Configure expression used to find field name
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Expression used to find field name</param>
        public static IFacetRangeParameter<TDocument> FieldExpression<TDocument>(this IFacetRangeParameter<TDocument> parameter, Expression<Func<TDocument, object>> value)
            where TDocument : Document
        {
            parameter.FieldExpression = value;

            return parameter;
        }

        /// <summary>
        /// Configure size of each range bucket to make facet
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Size of each range bucket to make facet</param>
        public static IFacetRangeParameter<TDocument> Gap<TDocument>(this IFacetRangeParameter<TDocument> parameter, string value)
            where TDocument : Document
        {
            parameter.Gap = value;

            return parameter;
        }

        /// <summary>
        /// Configure lower bound to make facet
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Lower bound to make facet</param>
        public static IFacetRangeParameter<TDocument> Start<TDocument>(this IFacetRangeParameter<TDocument> parameter, string value)
            where TDocument : Document
        {
            parameter.Start = value;

            return parameter;
        }

        /// <summary>
        /// Configure upper bound to make facet
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Upper bound to make facet</param>
        public static IFacetRangeParameter<TDocument> End<TDocument>(this IFacetRangeParameter<TDocument> parameter, string value)
            where TDocument : Document
        {
            parameter.End = value;

            return parameter;
        }

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Sort type of result of facet</param>
        public static IFacetRangeParameter<TDocument> SortType<TDocument>(this IFacetRangeParameter<TDocument> parameter, FacetSortType value)
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
        public static IFacetRangeParameter<TDocument> Excludes<TDocument>(this IFacetRangeParameter<TDocument> parameter, params string[] values)
            where TDocument : Document
        {
            parameter.Excludes = values;

            return parameter;
        }

        /// <summary>
        /// Configure counts should also be computed for all records with field values lower then lower bound of the first range
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Counts should also be computed for all records with field values lower then lower bound of the first range</param>
        public static IFacetRangeParameter<TDocument> CountBefore<TDocument>(this IFacetRangeParameter<TDocument> parameter, bool value)
            where TDocument : Document
        {
            parameter.CountBefore = value;

            return parameter;
        }

        /// <summary>
        /// Configure counts should also be computed for all records with field values greater then the upper bound of the last range
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Counts should also be computed for all records with field values greater then the upper bound of the last range</param>
        public static IFacetRangeParameter<TDocument> CountAfter<TDocument>(this IFacetRangeParameter<TDocument> parameter, bool value)
            where TDocument : Document
        {
            parameter.CountAfter = value;

            return parameter;
        }

        /// <summary>
        /// Configure minimum count of itens in facet's result
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Minimum count of itens in facet's result</param>
        public static IFacetRangeParameter<TDocument> Minimum<TDocument>(this IFacetRangeParameter<TDocument> parameter, int value)
            where TDocument : Document
        {
            parameter.Minimum = value;

            return parameter;
        }

        /// <summary>
        /// Configure limit of itens in facet's result
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Limit of itens in facet's result</param>
        public static IFacetRangeParameter<TDocument> Limit<TDocument>(this IFacetRangeParameter<TDocument> parameter, int value)
            where TDocument : Document
        {
            parameter.Limit = value;

            return parameter;
        }

        /// <summary>
        /// Configure filter or list of filters to be intersected with the incoming domain before faceting
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="configureFilter"></param>
        /// <returns></returns>
        public static IFacetRangeParameter<TDocument> Filter<TDocument>(this IFacetRangeParameter<TDocument> parameter, Action<SearchQuery<TDocument>> configureFilter)
            where TDocument : Document
        {
            var searchQuery = parameter.ServiceProvider.GetService<SearchQuery<TDocument>>();

            configureFilter.Invoke(searchQuery);

            parameter.Filter = searchQuery;

            return parameter;
        }

        /// <summary>
        /// Configure value of hardend
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="minimum">True to last bucket will end at “end” even if it is less than “gap” wide</param>
        public static IFacetRangeParameter<TDocument> HardEnd<TDocument>(this IFacetRangeParameter<TDocument> parameter, bool value)
            where TDocument : Document
        {
            parameter.HardEnd = value;

            return parameter;
        }
    }
}