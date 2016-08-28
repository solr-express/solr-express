namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet limit parameter
    /// </summary>
    public interface IFacetLimitParameter : ISearchParameter
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        IFacetLimitParameter Configure(int value);

        /// <summary>
        /// Value of limit
        /// </summary>
        int Value { get; }
    }
}
