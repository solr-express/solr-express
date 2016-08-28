namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in sort parameter
    /// </summary>
    public interface IRandomSortParameter : ISearchParameter
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        IRandomSortParameter Configure(bool ascendent);

        /// <summary>
        /// True to ascendent order, otherwise false
        /// </summary>
        bool Ascendent { get; }
    }
}
