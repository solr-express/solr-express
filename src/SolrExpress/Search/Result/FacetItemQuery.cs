using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet item of type Facet=Query
    /// </summary>
    public class FacetItemQuery : IFacetItem
    {
        /// <summary>
        /// Create a new instance of FacetItemQuery
        /// </summary>
        /// <param name="name">Name of facet</param>
        /// <param name="quantity">Quantity of item</param>
        public FacetItemQuery(string name, long quantity)
        {
            this.Name = name;
            this.FacetType = FacetType.Query;
            this.Quantity = quantity;
        }

        /// <summary>
        /// Name of facet
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of facet
        /// </summary>
        public FacetType FacetType { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public object Tag { get; set; }

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
