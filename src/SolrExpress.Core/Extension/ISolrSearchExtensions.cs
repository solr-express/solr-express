using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.ParameterValue;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Extension
{
    public static class ISolrSearchExtensions
    {
        /// <summary>
        /// Returns ISearchParameterBuilder instance
        /// </summary>
        /// <returns>ISearchParameterBuilder instance</returns>
        private static ISearchParameterBuilder<TDocument> GetBuilder<TDocument>(this ISolrSearch<TDocument> search)
            where TDocument : IDocument
        {
            return search.Engine.GetService<ISearchParameterBuilder<TDocument>>();
        }

        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static ISolrSearch<TDocument> FacetField<TDocument>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, FacetSortType? sortType = null, int? limit = null, params string[] excludes)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().FacetField(expression, sortType, limit, excludes));
            return search;
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static ISolrSearch<TDocument> FacetQuery<TDocument>(this ISolrSearch<TDocument> search, string aliasName, ISearchParameterValue<TDocument> query, FacetSortType? sortType = null, params string[] excludes)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().FacetQuery(aliasName, query, sortType, excludes));
            return search;
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="countBefore">Counts should also be computed for all records with field values lower then lower bound of the first range</param>
        /// <param name="countAfter">Counts should also be computed for all records with field values greater then the upper bound of the last range</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static ISolrSearch<TDocument> FacetRange<TDocument>(this ISolrSearch<TDocument> search, string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, bool countBefore = false, bool countAfter = false, FacetSortType? sortType = null, params string[] excludes)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().FacetRange(aliasName, expression, gap, start, end, countBefore, countAfter, sortType, excludes));
            return search;
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public static ISolrSearch<TDocument> Fields<TDocument>(this ISolrSearch<TDocument> search, params Expression<Func<TDocument, object>>[] expressions)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Fields(expressions));
            return search;
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="value">Value of the filter</param>
        public static ISolrSearch<TDocument> Filter<TDocument>(this ISolrSearch<TDocument> search, ISearchParameterValue<TDocument> value)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Filter(value));
            return search;
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public static ISolrSearch<TDocument> Filter<TDocument>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, string value, string tagName = null)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Filter(expression, value, tagName));
            return search;
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public static ISolrSearch<TDocument> Filter<TDocument, TValue>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, TValue? from, TValue? to, string tagName = null)
            where TDocument : IDocument
            where TValue : struct
        {
            search.Add(search.GetBuilder().Filter(expression, from, to, tagName));
            return search;
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static ISolrSearch<TDocument> Limit<TDocument>(this ISolrSearch<TDocument> search, int value)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Limit(value));
            return search;
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static ISolrSearch<TDocument> Offset<TDocument>(this ISolrSearch<TDocument> search, int value)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Offset(value));
            return search;
        }

        /// <summary>
        /// Create a offset/limit parameters
        /// </summary>
        /// <param name="limit">Value of limit</param>
        /// <param name="page">Value of page</param>
        public static ISolrSearch<TDocument> Page<TDocument>(this ISolrSearch<TDocument> search, int limit, int page)
            where TDocument : IDocument
        {
            var parameterBuilder = search.GetBuilder();
            search.Add(parameterBuilder.Limit(limit));
            search.Add(parameterBuilder.Offset((page - 1) * limit));

            return search;
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public static ISolrSearch<TDocument> QueryAll<TDocument>(this ISolrSearch<TDocument> search)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Query(new QueryAll<TDocument>()));
            return search;
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public static ISolrSearch<TDocument> Query<TDocument>(this ISolrSearch<TDocument> search, ISearchParameterValue<TDocument> value)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Query(value));
            return search;
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public static ISolrSearch<TDocument> Query<TDocument>(this ISolrSearch<TDocument> search, string value)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Query(value));
            return search;
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the query</param>
        public static ISolrSearch<TDocument> Query<TDocument>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, string value)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Query(value));
            return search;
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public static ISolrSearch<TDocument> Sort<TDocument>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, bool ascendent)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Sort(expression, ascendent));
            return search;
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="expressions">Expression used to find the property name</param>
        public static ISolrSearch<TDocument> Sort<TDocument>(this ISolrSearch<TDocument> search, bool ascendent, params Expression<Func<TDocument, object>>[] expressions)
            where TDocument : IDocument
        {
            foreach (var item in search.GetBuilder().Sort(ascendent, expressions))
            {
                search.Add(item);
            }

            return search;
        }

        /// <summary>
        /// Create a random sort parameter
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="expressions">Expression used to find the property name</param>
        public static ISolrSearch<TDocument> RandomSort<TDocument>(this ISolrSearch<TDocument> search, bool ascendent)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().RandomSort(ascendent));
            return search;
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static ISolrSearch<TDocument> FacetLimit<TDocument>(this ISolrSearch<TDocument> search, int value)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().FacetLimit(value));
            return search;
        }

        /// <summary>
        /// Create a minimum should match parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public static ISolrSearch<TDocument> MinimumShouldMatch<TDocument>(this ISolrSearch<TDocument> search, string expression)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().MinimumShouldMatch(expression));
            return search;
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public static ISolrSearch<TDocument> QueryField<TDocument>(this ISolrSearch<TDocument> search, string expression)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().QueryField(expression));
            return search;
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        public static ISolrSearch<TDocument> SpatialFilter<TDocument>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, SolrSpatialFunctionType functionType, GeoCoordinate centerPoint, decimal distance)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().SpatialFilter(expression, functionType, centerPoint, distance));
            return search;
        }

        /// <summary>
        /// Create a any parameter
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        public static ISolrSearch<TDocument> Any<TDocument>(this ISolrSearch<TDocument> search, string name, string value)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Any(name, value));
            return search;
        }

        /// <summary>
        /// Create a boost parameter
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        /// <param name="boostFunctionType">Boost type used in calculation. Default is BoostFunctionType.Boost</param>
        public static ISolrSearch<TDocument> Boost<TDocument>(this ISolrSearch<TDocument> search, ISearchParameterValue<TDocument> query, BoostFunctionType? boostFunctionType = null)
            where TDocument : IDocument
        {
            search.Add(search.GetBuilder().Boost(query, boostFunctionType ?? BoostFunctionType.Boost));
            return search;
        }
    }
}
