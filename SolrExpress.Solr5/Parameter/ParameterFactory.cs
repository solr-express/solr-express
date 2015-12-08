using SolrExpress.Core.Entity;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.Query;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Parameter
{
    /// <summary>
    /// Signatures of SOLR common paramaters factory 
    /// </summary>
    public class ParameterFactory<TDocument> : IParameterFactory<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Get a facet field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of facet's result</param>
        /// <param name="limit">Limit of facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public IFacetFieldParameter GetFacetFieldParameter(Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType = null, int? limit = null, params string[] excludes)
        {
            return new FacetFieldParameter<TDocument>(expression, sortType, limit, excludes);
        }

        /// <summary>
        /// Get a facet query parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public IFacetQueryParameter GetFacetQueryParameter(string aliasName, IQueryParameterValue query, SolrFacetSortType? sortType = null, params string[] excludes)
        {
            return new FacetQueryParameter(aliasName, query, sortType, excludes);
        }

        /// <summary>
        /// Get a facet range parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public IFacetRangeParameter GetFacetRangeParameter(string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, SolrFacetSortType? sortType = null, params string[] excludes)
        {
            return new FacetRangeParameter<TDocument>(aliasName, expression, gap, start, end, sortType, excludes);
        }

        /// <summary>
        /// Get a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public IFieldsParameter GetFieldsParameter(params Expression<Func<TDocument, object>>[] expressions)
        {
            return new FieldsParameter<TDocument>(expressions);
        }

        /// <summary>
        /// Get a filter parameter
        /// </summary>
        /// <param name="value">Parameter value used to create the query</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public IFilterParameter GetFilterParameter(IQueryParameterValue value, string tagName = null)
        {
            return new FilterParameter(value, tagName);
        }

        /// <summary>
        /// Get a limit parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public ILimitParameter GetLimitParameter(int value)
        {
            return new LimitParameter(value);
        }

        /// <summary>
        /// Get a offset parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public IOffsetParameter GetOffsetParameter(int value)
        {
            return new OffsetParameter(value);
        }

        /// <summary>
        /// Get a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public IQueryParameter GetQueryParameter(IQueryParameterValue value)
        {
            return new QueryParameter(value);
        }

        /// <summary>
        /// Get a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public ISortParameter GetSortParameter(Expression<Func<TDocument, object>> expression, bool ascendent)
        {
            return new SortParameter<TDocument>(expression, ascendent);
        }

        /// <summary>
        /// Get a facet limit parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public IFacetLimitParameter GetFacetLimitParameter(int value)
        {
            return new FacetLimitParameter(value);
        }
    }
}
