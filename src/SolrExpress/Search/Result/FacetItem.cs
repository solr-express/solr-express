namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet item
    /// </summary>
    public abstract class FacetItem
    {
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
    }
}
