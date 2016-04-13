using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Limit of itens in facet's result
        /// </summary>
        int? Limit { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        List<string> Excludes { get; set; }
    }
}
