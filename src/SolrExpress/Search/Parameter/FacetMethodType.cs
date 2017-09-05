namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Method's type to how to facet the field
    /// </summary>
    public enum FacetMethodType
    {
        /// <summary>
        /// Stands for UninvertedField, a method of faceting indexed, multi-valued fields using top-level data structures that optimize for performance over NRT capabilities
        /// </summary>
        UninvertedField,

        /// <summary>
        /// Stands for DocValues, a method of faceting indexed, multi-valued fields using per-segment data structures
        /// </summary>
        DocValues,

        /// <summary>
        /// Creates each individual facet bucket (including any sub-facets) on-the-fly while streaming the response back to the requester
        /// </summary>
        Stream
    }
}
