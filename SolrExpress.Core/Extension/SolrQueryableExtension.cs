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
        public static SolrQueryable<TDocument> FacetField<TDocument>(this SolrQueryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType = null)
            where TDocument : IDocument
        {
            var parameter = queryable.Resolver.GetParameter<IFacetFieldParameter<TDocument>, FacetFieldSettings<TDocument>>(new FacetFieldSettings<TDocument>
            {
                Expression = expression,
                SortType = sortType
            });

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public static SolrQueryable<TDocument> FacetQuery<TDocument>(this SolrQueryable<TDocument> queryable, string aliasName, IQueryParameterValue query, SolrFacetSortType? sortType = null)
            where TDocument : IDocument
        {
            var parameter = queryable.Resolver.GetParameter<IFacetQueryParameter<TDocument>>();
            parameter.AliasName = aliasName;
            parameter.Query = query;
            parameter.SortType = sortType;

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
        public static SolrQueryable<TDocument> FacetRange<TDocument>(this SolrQueryable<TDocument> queryable, string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, SolrFacetSortType? sortType = null)
            where TDocument : IDocument
        {
            var parameter = queryable.Resolver.GetParameter<IFacetRangeParameter<TDocument>>();
            parameter.AliasName = aliasName;
            parameter.Expression = expression;
            parameter.Gap = gap;
            parameter.Start = start;
            parameter.End = end;
            parameter.SortType = sortType;

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public static SolrQueryable<TDocument> Fields<TDocument>(this SolrQueryable<TDocument> queryable, params Expression<Func<TDocument, object>>[] expressions)
            where TDocument : IDocument
        {
            var parameter = queryable.Resolver.GetParameter<IFieldsParameter<TDocument>>();
            parameter.Expressions.AddRange(expressions);

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        public static SolrQueryable<TDocument> Filter<TDocument>(this SolrQueryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, string value)
            where TDocument : IDocument
        {
            var paramaterValue = new Single<TDocument>(expression, value);

            var parameter = queryable.Resolver.GetParameter<IFilterParameter<TDocument>>();
            parameter.Expression = expression;
            parameter.Value = paramaterValue;

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public static SolrQueryable<TDocument> Filter<TDocument, TValue>(this SolrQueryable<TDocument> queryable, Expression<Func<TDocument, object>> expression, TValue? from, TValue? to)
            where TDocument : IDocument
            where TValue : struct
        {
            var paramaterValue = new Range<TDocument, TValue>(expression, from, to);

            var parameter = queryable.Resolver.GetParameter<IFilterParameter<TDocument>>();
            parameter.Expression = expression;
            parameter.Value = paramaterValue;

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static SolrQueryable<TDocument> Limit<TDocument>(this SolrQueryable<TDocument> queryable, int value)
            where TDocument : IDocument
        {
            var parameter = queryable.Resolver.GetParameter<ILimitParameter>();
            parameter.Value = value;

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static SolrQueryable<TDocument> Offset<TDocument>(this SolrQueryable<TDocument> queryable, int value)
            where TDocument : IDocument
        {
            var parameter = queryable.Resolver.GetParameter<IOffsetParameter>();
            parameter.Value = value;

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public static SolrQueryable<TDocument> Query<TDocument>(this SolrQueryable<TDocument> queryable, IQueryParameterValue value)
            where TDocument : IDocument
        {
            var parameter = queryable.Resolver.GetParameter<IQueryParameter<TDocument>>();
            parameter.Value = value;

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

            var parameter = queryable.Resolver.GetParameter<IQueryParameter<TDocument>>();
            parameter.Value = paramaterValue;

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

            var parameter = queryable.Resolver.GetParameter<IQueryParameter<TDocument>>();
            parameter.Value = paramaterValue;

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
            var parameter = queryable.Resolver.GetParameter<ISortParameter<TDocument>>();
            parameter.Expression = expression;
            parameter.Ascendent = ascendent;

            return queryable.Parameter(parameter);
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static SolrQueryable<TDocument> FacetLimit<TDocument>(this SolrQueryable<TDocument> queryable, int value)
            where TDocument : IDocument
        {
            var parameter = queryable.Resolver.GetParameter<IFacetLimitParameter>();
            parameter.Value = value;

            return queryable.Parameter(parameter);
        }
    }
}
