using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Query;
using SolrExpress.Solr5.Parameter;
using SolrExpress.Solr5.ParameterValue;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Linq
{
    /// <summary>
    /// SOLR queryable extensions methods used to create facilities in the use of the classes
    /// </summary>
    public static class SolrQueryableExtension
    {
        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public static SolrQueryable<TDocument> FacetField<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType = null)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new FacetFieldParameter<TDocument>(expression, sortType));
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public static SolrQueryable<TDocument> FacetQuery<TDocument>(this SolrQueryable<TDocument> solrQueryable, string aliasName, string query, SolrFacetSortType? sortType = null)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new FacetQueryParameter(aliasName, query, sortType));
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public static SolrQueryable<TDocument> FacetRange<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, string aliasName, string gap = null, string start = null, string end = null, SolrFacetSortType? sortType = null)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new FacetRangeParameter<TDocument>(expression, aliasName, gap, start, end, sortType));
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        public static SolrQueryable<TDocument> Fields<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new FieldsParameter<TDocument>(expression));
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        public static SolrQueryable<TDocument> Filter<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, string value)
            where TDocument : IDocument
        {
            var paramaterValue = new SingleValue<TDocument>(expression, value);
            return solrQueryable.Parameter(new FilterParameter(paramaterValue));
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public static SolrQueryable<TDocument> Filter<TDocument, TValue>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, TValue? from, TValue? to)
            where TDocument : IDocument
            where TValue : struct
        {
            var value = new RangeValue<TDocument, TValue>(expression, from, to);
            return solrQueryable.Parameter(new FilterParameter(value));
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public static SolrQueryable<TDocument> SpatialFilter<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, GeoCoordinate? from, GeoCoordinate? to)
            where TDocument : IDocument
        {
            var value = new RangeValue<TDocument, GeoCoordinate>(expression, from, to);
            return solrQueryable.Parameter(new SpatialFilterParameter(value));
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="value">Parameter to include in the query</param>
        public static SolrQueryable<TDocument> Limit<TDocument>(this SolrQueryable<TDocument> solrQueryable, int value)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new LimitParameter(value));
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="value">Parameter to include in the query</param>
        public static SolrQueryable<TDocument> Offset<TDocument>(this SolrQueryable<TDocument> solrQueryable, int value)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new OffsetParameter(value));
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="value">Parameter to include in the query</param>
        public static SolrQueryable<TDocument> Query<TDocument>(this SolrQueryable<TDocument> solrQueryable, string value)
            where TDocument : IDocument
        {
            var freeValue = new FreeValue(value);
            return solrQueryable.Parameter(new QueryParameter(freeValue));
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the query</param>
        public static SolrQueryable<TDocument> Query<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, string value)
            where TDocument : IDocument
        {
            var paramaterValue = new SingleValue<TDocument>(expression, value);
            return solrQueryable.Parameter(new QueryParameter(paramaterValue));
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public static SolrQueryable<TDocument> Sort<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, bool ascendent)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new SortParameter<TDocument>(expression, ascendent));
        }
    }
}
