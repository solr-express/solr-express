using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;
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
        public static SolrQueryable<TDocument> FacetField<TDocument>(this SolrQueryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, FacetSortType? sortType = null, int? limit = null, params string[] excludes)
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
        public static SolrQueryable<TDocument> FacetQuery<TDocument>(this SolrQueryable<TDocument> queryable, string aliasName, IQueryParameterValue query, FacetSortType? sortType = null, params string[] excludes)
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
        public static SolrQueryable<TDocument> FacetRange<TDocument>(this SolrQueryable<TDocument> queryable, string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, FacetSortType? sortType = null)
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
        public static SolrQueryable<TDocument> Fields<TDocument>(this SolrQueryable<TDocument> queryable, params Expression<Func<TDocument, object>>[] expressions)
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
        public static SolrQueryable<TDocument> Filter<TDocument>(this SolrQueryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, string value, string tagName = null)
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
        public static SolrQueryable<TDocument> Filter<TDocument, TValue>(this SolrQueryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, TValue? from, TValue? to, string tagName = null)
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
        public static SolrQueryable<TDocument> Limit<TDocument>(this SolrQueryable<TDocument> queryable, int value)
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
        public static SolrQueryable<TDocument> Offset<TDocument>(this SolrQueryable<TDocument> queryable, int value)
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
        public static SolrQueryable<TDocument> Query<TDocument>(this SolrQueryable<TDocument> queryable, IQueryParameterValue value)
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
        public static SolrQueryable<TDocument> Query<TDocument>(this SolrQueryable<TDocument> queryable, string value)
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
        public static SolrQueryable<TDocument> Query<TDocument>(this SolrQueryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, string value)
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
        public static SolrQueryable<TDocument> Sort<TDocument>(this SolrQueryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, bool ascendent)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<ISortParameter<TDocument>>()
                .Configure(expression, ascendent);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="expressions">Expression used to find the property name</param>
        public static SolrQueryable<TDocument> Sort<TDocument>(this SolrQueryable<TDocument> queryable, bool ascendent, params Expression<Func<TDocument, object>>[] expressions)
            where TDocument : IDocument
        {
            foreach (var expression in expressions)
            {
                var parameter = queryable
                    .Resolver
                    .GetInstance<ISortParameter<TDocument>>()
                    .Configure(expression, ascendent);

                queryable.Parameter(parameter);
            }

            return queryable;
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static SolrQueryable<TDocument> FacetLimit<TDocument>(this SolrQueryable<TDocument> queryable, int value)
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
        public static SolrQueryable<TDocument> MinimumShouldMatch<TDocument>(this SolrQueryable<TDocument> queryable, string expression)
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
        public static SolrQueryable<TDocument> QueryField<TDocument>(this SolrQueryable<TDocument> queryable, string expression)
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
        public static SolrQueryable<TDocument> SpatialFilter<TDocument>(this SolrQueryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, SolrSpatialFunctionType functionType, GeoCoordinate centerPoint, decimal distance)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<ISpatialFilterParameter<TDocument>>()
                .Configure(expression, functionType, centerPoint, distance);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a any parameter
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        public static SolrQueryable<TDocument> Any<TDocument>(this SolrQueryable<TDocument> queryable, string name, string value)
            where TDocument : IDocument
        {
            var parameter = queryable
                .Resolver
                .GetInstance<IAnyParameter>()
                .Configure(name, value);

            return queryable.Parameter(parameter);
        }
    }
}
