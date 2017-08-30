using SolrExpress.Search.Query;
using System;

namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure in facet query parameter
    /// </summary>
    public static class IFacetQueryParameterExtension

    {
        /// <summary>
        /// Configure name of alias added in query
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="aliasName">Name of alias added in query</param>
        public static IFacetQueryParameter<TDocument> AliasName<TDocument>(this IFacetQueryParameter<TDocument> parameter, string aliasName)
            where TDocument : Document
        {
            parameter.AliasName = aliasName;

            return parameter;
        }

        /// <summary>
        /// Configure query used to make facet
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="query">Query used to make facet</param>
        public static IFacetQueryParameter<TDocument> Query<TDocument>(this IFacetQueryParameter<TDocument> parameter, SearchQuery<TDocument> query)
            where TDocument : Document
        {
            parameter.Query = query;

            return parameter;
        }

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="sortType">Sort type of result of facet</param>
        public static IFacetQueryParameter<TDocument> SortType<TDocument>(this IFacetQueryParameter<TDocument> parameter, FacetSortType sortType)
            where TDocument : Document
        {
            parameter.SortType = sortType;

            return parameter;
        }

        /// <summary>
        /// Configure list of tags to exclude in facet calculation
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static IFacetQueryParameter<TDocument> Excludes<TDocument>(this IFacetQueryParameter<TDocument> parameter, params string[] excludes)
            where TDocument : Document
        {
            parameter.Excludes = excludes;

            return parameter;
        }

        /// <summary>
        /// Configure minimum count of itens in facet's result
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="minimum">Minimum count of itens in facet's result</param>
        public static IFacetQueryParameter<TDocument> Minimum<TDocument>(this IFacetQueryParameter<TDocument> parameter, int minimum)
            where TDocument : Document
        {
            parameter.Minimum = minimum;

            return parameter;
        }

        /// <summary>
        /// Configure limit of itens in facet's result
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        public static IFacetQueryParameter<TDocument> Limit<TDocument>(this IFacetQueryParameter<TDocument> parameter, int limit)
            where TDocument : Document
        {
            parameter.Limit = limit;

            return parameter;
        }

        /// <summary>
        /// Configure filter or list of filters to be intersected with the incoming domain before faceting
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="configureFilter"></param>
        /// <returns></returns>
        public static IFacetQueryParameter<TDocument> Filter<TDocument>(this IFacetQueryParameter<TDocument> parameter, Action<SearchQuery<TDocument>> configureFilter)
            where TDocument : Document
        {
            var searchQuery = parameter.ServiceProvider.GetService<SearchQuery<TDocument>>();

            configureFilter.Invoke(searchQuery);

            parameter.Filter = searchQuery;

            return parameter;
        }
    }
}
