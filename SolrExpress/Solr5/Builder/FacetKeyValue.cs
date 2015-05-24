using System.Collections.Generic;

namespace SolrExpress.Solr5.Builder
{
    /// <summary>
    /// Represents a facet values
    /// </summary>
    /// <typeparam name="TKey">Value of the facet</typeparam>
    public sealed class FacetKeyValue<TKey>
    {
        /// <summary>
        /// Name of the facet
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of data of the facet
        /// </summary>
        public Dictionary<TKey, long> Data { get; set; }
    }
}
