namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use in facet limit parameter
    /// </summary>
    public interface IFacetLimitParameter : IParameter
    {
        /// <summary>
        /// Value of limit
        /// </summary>
        int Value { get; set; }
    }
}
