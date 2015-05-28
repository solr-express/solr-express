using SolrExpress.Enumerator;
using SolrExpress.Query;
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
        /// <param name="sortAscending">Sort ascending the result of the facet</param>
        public static SolrQueryable<TDocument> FacetField<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType = null, bool? sortAscending = true)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new Solr5.Parameter.FacetFieldParameter<TDocument>(expression, sortType, sortAscending));
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="sortAscending">Sort ascending the result of the facet</param>
        public static SolrQueryable<TDocument> FacetQuery<TDocument>(this SolrQueryable<TDocument> solrQueryable, string aliasName, string query, SolrFacetSortType? sortType = null, bool? sortAscending = true)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new Solr5.Parameter.FacetQueryParameter(aliasName, query, sortType, sortAscending));
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
        /// <param name="sortAscending">Sort ascending the result of the facet</param>
        public static SolrQueryable<TDocument> FacetRange<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, string aliasName, string gap = null, string start = null, string end = null, SolrFacetSortType? sortType = null, bool? sortAscending = null)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new Solr5.Parameter.FacetRangeParameter<TDocument>(expression, aliasName, gap, start, end, sortType, sortAscending));
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        public static SolrQueryable<TDocument> Fields<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new Solr5.Parameter.FieldsParameter<TDocument>(expression));
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
            return solrQueryable.Parameter(new Solr5.Parameter.FilterParameter<TDocument>(expression, value));
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public static SolrQueryable<TDocument> Filter<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, DateTime? from, DateTime? to)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new Solr5.Parameter.FilterParameter<TDocument>(expression, from, to));
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public static SolrQueryable<TDocument> Filter<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, int? from, int? to)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new Solr5.Parameter.FilterParameter<TDocument>(expression, from, to));
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public static SolrQueryable<TDocument> Filter<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, double? from, double? to)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new Solr5.Parameter.FilterParameter<TDocument>(expression, from, to));
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public static SolrQueryable<TDocument> Filter<TDocument>(this SolrQueryable<TDocument> solrQueryable, Expression<Func<TDocument, object>> expression, decimal? from, decimal? to)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new Solr5.Parameter.FilterParameter<TDocument>(expression, from, to));
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="value">Parameter to include in the query</param>
        public static SolrQueryable<TDocument> Limit<TDocument>(this SolrQueryable<TDocument> solrQueryable, int value)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new Solr5.Parameter.LimitParameter(value));
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="value">Parameter to include in the query</param>
        public static SolrQueryable<TDocument> Offset<TDocument>(this SolrQueryable<TDocument> solrQueryable, int value)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new Solr5.Parameter.OffsetParameter(value));
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="solrQueryable">The solr query</param>
        /// <param name="value">Parameter to include in the query</param>
        public static SolrQueryable<TDocument> Query<TDocument>(this SolrQueryable<TDocument> solrQueryable, string value)
            where TDocument : IDocument
        {
            return solrQueryable.Parameter(new Solr5.Parameter.QueryParameter<TDocument>(value));
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
            return solrQueryable.Parameter(new Solr5.Parameter.QueryParameter<TDocument>(value));
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
            return solrQueryable.Parameter(new Solr5.Parameter.SortParameter<TDocument>(expression, ascendent));
        }
    }
}
