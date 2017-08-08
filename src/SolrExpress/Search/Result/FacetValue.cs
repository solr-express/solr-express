using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet item value
    /// </summary>
    public abstract class FacetValue
    {
        /// <summary>
        /// Quantity of item
        /// </summary>
        public long Quantity { get; set; }

        /// <summary>
        /// Subfacets of item
        /// </summary>
        public IEnumerable<FacetItem> Facets { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public object Tag { get; set; }
    }
}
