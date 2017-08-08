using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet item with multiple values
    /// </summary>
    /// <typeparam name="TKey">Value of the facet</typeparam>
    public sealed class FacetItemMultiValues<TKey> : FacetItem
    {
        public FacetItemMultiValues()
        {
            this.Data = new List<FacetValue<TKey>>();
        }

        /// <summary>
        /// Data of facet
        /// </summary>
        public IEnumerable<FacetValue<TKey>> Data { get; set; }
    }
}
