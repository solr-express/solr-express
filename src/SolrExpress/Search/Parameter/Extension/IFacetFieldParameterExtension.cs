using SolrExpress.Search.Query;
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
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Expression used to find field name</param>
        public static IFacetFieldParameter<TDocument> FieldExpression<TDocument>(this IFacetFieldParameter<TDocument> parameter, Expression<Func<TDocument, object>> value)
            where TDocument : Document
        {
            parameter.FieldExpression = value;

            return parameter;
        }

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Sort type of result of facet</param>
        public static IFacetFieldParameter<TDocument> SortType<TDocument>(this IFacetFieldParameter<TDocument> parameter, FacetSortType value)
            where TDocument : Document
        {
            parameter.SortType = value;

            return parameter;
        }

        /// <summary>
        /// Configure minimum count of itens in facet's result
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Minimum count of itens in facet's result</param>
        public static IFacetFieldParameter<TDocument> Minimum<TDocument>(this IFacetFieldParameter<TDocument> parameter, int value)
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
        public static IFacetFieldParameter<TDocument> Limit<TDocument>(this IFacetFieldParameter<TDocument> parameter, int value)
            where TDocument : Document
        {
            parameter.Limit = value;

            return parameter;
        }

        /// <summary>
        /// Configure list of tags to exclude in facet calculation
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="values">List of tags to exclude in facet calculation</param>
        public static IFacetFieldParameter<TDocument> Excludes<TDocument>(this IFacetFieldParameter<TDocument> parameter, params string[] values)
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
        /// <param name="configureFilter"></param>
        /// <returns></returns>
        public static IFacetFieldParameter<TDocument> Filter<TDocument>(this IFacetFieldParameter<TDocument> parameter, Action<SearchQuery<TDocument>> configureFilter)
            where TDocument : Document
        {
            var searchQuery = parameter.ServiceProvider.GetService<SearchQuery<TDocument>>();

            configureFilter.Invoke(searchQuery);

            parameter.Filter = searchQuery;

            return parameter;
        }

        /// <summary>
        /// Configure method type to how to facet the field
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Method type to how to facet the field</param>
        public static IFacetFieldParameter<TDocument> MethodType<TDocument>(this IFacetFieldParameter<TDocument> parameter, FacetMethodType value)
            where TDocument : Document
        {
            parameter.MethodType = value;

            return parameter;
        }

        /// <summary>
        /// Configure prefix to only produce buckets for terms starting with the specified value
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Prefix to only produce buckets for terms starting with the specified value</param>
        public static IFacetFieldParameter<TDocument> Prefix<TDocument>(this IFacetFieldParameter<TDocument> parameter, string value)
            where TDocument : Document
        {
            parameter.Prefix = value;

            return parameter;
        }
    }
}
