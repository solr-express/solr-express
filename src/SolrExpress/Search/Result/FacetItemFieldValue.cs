using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a value of a facet item of type Facet=Field
    /// </summary>
    public class FacetItemFieldValue
    {
        /// <summary>
        /// Key of item
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Quantity of item
        /// </summary>
        public long Quantity { get; set; }

        /// <summary>
        /// Subfacets of item
        /// </summary>
        public IEnumerable<IFacetItem> Facets { get; set; }
    }
}
