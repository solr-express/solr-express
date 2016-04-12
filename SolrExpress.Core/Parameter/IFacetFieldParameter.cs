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
        /// Expression used to find the property name
        /// </summary>
        Expression<Func<TDocument, object>> Expression { get; set; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        SolrFacetSortType? SortType { get; set; }
    }
}
