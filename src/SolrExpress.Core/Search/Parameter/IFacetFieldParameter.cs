using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use infacet field parameter
    /// </summary>
    public interface IFacetFieldParameter<TDocument> : ISearchParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetFieldParameter<TDocument> Configure(Expression<Func<TDocument, object>> expression, FacetSortType? sortType = null, int? limit = null, params string[] excludes);

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        Expression<Func<TDocument, object>> Expression { get; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        FacetSortType? SortType { get; }

        /// <summary>
        /// Limit of itens in facet's result
        /// </summary>
        int? Limit { get; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        string[] Excludes { get; }
    }
}
