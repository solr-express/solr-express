using SolrExpress.Core.Parameter;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Core.Query;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Extension
{
    public static class SolrQueryableExtension
    {
        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static Queryable<TDocument> FacetField<TDocument>(this Queryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType = null, int? limit = null, params string[] excludes)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<IFacetFieldParameter<TDocument>>()
                .Configure(expression, sortType, limit, excludes);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static Queryable<TDocument> FacetQuery<TDocument>(this Queryable<TDocument> queryable, string aliasName, IQueryParameterValue query, SolrFacetSortType? sortType = null, params string[] excludes)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<IFacetQueryParameter<TDocument>>()
                .Configure(aliasName, query, sortType, excludes);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public static Queryable<TDocument> FacetRange<TDocument>(this Queryable<TDocument> queryable, string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, SolrFacetSortType? sortType = null)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<IFacetRangeParameter<TDocument>>()
                .Configure(aliasName, expression, gap, start, end, sortType);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public static Queryable<TDocument> Fields<TDocument>(this Queryable<TDocument> queryable, params Expression<Func<TDocument, object>>[] expressions)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<IFieldsParameter<TDocument>>()
                .Configure(expressions);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public static Queryable<TDocument> Filter<TDocument>(this Queryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, string value, string tagName = null)
            where TDocument : IDocument
        {
            var paramaterValue = new Single<TDocument>(expression, value);

            var parameter = queryable
                .Resolver
                .GetInstance<IFilterParameter<TDocument>>()
                .Configure(paramaterValue, tagName);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public static Queryable<TDocument> Filter<TDocument, TValue>(this Queryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, TValue? from, TValue? to, string tagName = null)
            where TDocument : IDocument
            where TValue : struct
        {
            var paramaterValue = new Range<TDocument, TValue>(expression, from, to);

            var parameter = queryable
                .Resolver
                .GetInstance<IFilterParameter<TDocument>>()
                .Configure(paramaterValue, tagName);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static Queryable<TDocument> Limit<TDocument>(this Queryable<TDocument> queryable, int value)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<ILimitParameter>()
                .Configure(value);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static Queryable<TDocument> Offset<TDocument>(this Queryable<TDocument> queryable, int value)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<IOffsetParameter>()
                .Configure(value);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public static Queryable<TDocument> Query<TDocument>(this Queryable<TDocument> queryable, IQueryParameterValue value)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<IQueryParameter<TDocument>>()
                .Configure(value);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public static Queryable<TDocument> Query<TDocument>(this Queryable<TDocument> queryable, string value)
            where TDocument : IDocument
        {
            var paramaterValue = new Any(value);

            var parameter = queryable
                .Resolver
                .GetInstance<IQueryParameter<TDocument>>()
                .Configure(paramaterValue);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the query</param>
        public static Queryable<TDocument> Query<TDocument>(this Queryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, string value)
            where TDocument : IDocument
        {
            var paramaterValue = new Single<TDocument>(expression, value);

            var parameter = queryable
                .Resolver
                .GetInstance<IQueryParameter<TDocument>>()
                .Configure(paramaterValue);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public static Queryable<TDocument> Sort<TDocument>(this Queryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, bool ascendent)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<ISortParameter<TDocument>>()
                .Configure(expression, ascendent);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static Queryable<TDocument> FacetLimit<TDocument>(this Queryable<TDocument> queryable, int value)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<IFacetLimitParameter>()
                .Configure(value);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a minimum should match parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public static Queryable<TDocument> MinimumShouldMatch<TDocument>(this Queryable<TDocument> queryable, string expression)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<IMinimumShouldMatchParameter>()
                .Configure(expression);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public static Queryable<TDocument> QueryField<TDocument>(this Queryable<TDocument> queryable, string expression)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<IQueryFieldParameter>()
                .Configure(expression);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        public static Queryable<TDocument> SpatialFilter<TDocument>(this Queryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, SolrSpatialFunctionType functionType, GeoCoordinate centerPoint, decimal distance)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<ISpatialFilterParameter<TDocument>>()
                .Configure(expression, functionType, centerPoint, distance);

            return queryable.Parameter(parameter);
        }
    }
}
