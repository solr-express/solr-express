namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Signatures to represents a facet item
    /// </summary>
    public interface IFacetItem
    {
        /// <summary>
        /// Name of facet
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Type of facet
        /// </summary>
        FacetType FacetType { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        object Tag { get; set; }
    }
}
