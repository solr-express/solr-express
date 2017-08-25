namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet item
    /// </summary>
    public interface IFacetItem
    {
        /// <summary>
        /// Name of facet
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Type of facet
        /// </summary>
        FacetType FacetType { get; }

        /// <summary>
        /// Tag
        /// </summary>
        object Tag { get; }
    }
}
