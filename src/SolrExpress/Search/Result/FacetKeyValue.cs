using System.Collections.Generic;

namespace SolrExpress.Search.Result
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
        public IEnumerable<FacetItemValue<TKey>> Data { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public object Tag { get; set; }
    }
}
