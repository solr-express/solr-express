using SolrExpress.Core.Parameter;
using SolrExpress.Core.ParameterValue;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Factory
{
    /// <summary>
    /// Signatures of SOLR common paramaters factory 
    /// </summary>
    public interface IParameterFactory<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Get a facet field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of facet's result</param>
        /// <param name="limit">Limit of facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetFieldParameter GetFacetFieldParameter(Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType = null, int? limit = null, params string[] excludes);

        /// <summary>
        /// Get a facet query parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetQueryParameter GetFacetQueryParameter(string aliasName, IQueryParameterValue query, SolrFacetSortType? sortType = null, params string[] excludes);

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
        IFacetRangeParameter GetFacetRangeParameter(string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, SolrFacetSortType? sortType = null, params string[] excludes);

        /// <summary>
        /// Get a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        IFieldsParameter GetFieldsParameter(params Expression<Func<TDocument, object>>[] expressions);

        /// <summary>
        /// Get a filter parameter
        /// </summary>
        /// <param name="value">Parameter value used to create the query</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        IFilterParameter GetFilterParameter(IQueryParameterValue value, string tagName = null);

        /// <summary>
        /// Get a limit parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        ILimitParameter GetLimitParameter(int value);

        /// <summary>
        /// Get a offset parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        IOffsetParameter GetOffsetParameter(int value);

        /// <summary>
        /// Get a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        IQueryParameter GetQueryParameter(IQueryParameterValue value);

        /// <summary>
        /// Get a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        ISortParameter GetSortParameter(Expression<Func<TDocument, object>> expression, bool ascendent);

        /// <summary>
        /// Get a facet limit parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        IFacetLimitParameter GetFacetLimitParameter(int value);
    }
}
