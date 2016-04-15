using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use infacet field parameter
    /// </summary>
    public interface IFacetFieldParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetFieldParameter<TDocument> Configure(Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType, int? limit, string[] excludes);
    }
}
