using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet value
    /// </summary>
    /// <typeparam name="TKey">Value of the facet</typeparam>
    public sealed class FacetKeyValue<TKey> : FacetKeyValue
    {
        /// <summary>
        /// Data list of facet
        /// </summary>
        public IEnumerable<FacetItemValue<TKey>> Data { get; set; }
    }
}
