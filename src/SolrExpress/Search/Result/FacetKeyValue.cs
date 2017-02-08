namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet value
    /// </summary>
    public abstract class FacetKeyValue
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
