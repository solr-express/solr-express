using System.Collections.Generic;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Represents a facet value
    /// </summary>
    /// <typeparam name="TKey">Value of the facet</typeparam>
    public sealed class FacetKeyValue<TKey>
    {
        /// <summary>
        /// Name of the facet
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data list of the facet
        /// </summary>
        public Dictionary<TKey, long> Data { get; set; }
    }
}
