using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet value
    /// </summary>
    /// <typeparam name="TKey">Value of the facet</typeparam>
    public sealed class FacetKeyValue<TKey> : FacetKeyValue
    {
        public FacetKeyValue()
        {
            this.Data = new List<FacetItemValue<TKey>>();
        }

        /// <summary>
        /// Data of facet
        /// </summary>
        public IEnumerable<FacetItemValue<TKey>> Data { get; set; }
    }
}
