using System.Collections.Generic;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use facet parameter
    /// </summary>
    public interface IFacetParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Services provider
        /// </summary>
        ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }

        /// <summary>
        /// List of subfacets
        /// </summary>
        IList<IFacetParameter<TDocument>> Facets { get; set; }
    }
}
