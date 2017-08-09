using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet item of type Facet=Range
    /// </summary>
    public class FacetItemRange : IFacetItem
    {
        /// <summary>
        /// Create a new instance of FacetItemRange
        /// </summary>
        /// <param name="name">Name of facet</param>
        public FacetItemRange(string name)
        {
            this.Name = name;
            this.FacetType = FacetType.Range;
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
        /// Values of field
        /// </summary>
        public IEnumerable<IFacetItemRangeValue> Values { get; set; } = new List<IFacetItemRangeValue>();
    }
}
