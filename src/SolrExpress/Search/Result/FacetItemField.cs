using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet item of type Facet=Field
    /// </summary>
    public class FacetItemField : IFacetItem
    {
        /// <summary>
        /// Create a new instance of FacetItemField
        /// </summary>
        /// <param name="name">Name of facet</param>
        public FacetItemField(string name)
        {
            this.Name = name;
            this.FacetType = FacetType.Field;
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
        public IEnumerable<FacetItemFieldValue> Values { get; } = new List<FacetItemFieldValue>();
    }
}
