using System.Collections.Generic;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use facet parameter
    /// </summary>
    public interface IFacetParameter : ISearchParameter
    {
        /// <summary>
        /// List of subfacets
        /// </summary>
        IEnumerable<IFacetParameter> Facets { get; set; }
    }
}
