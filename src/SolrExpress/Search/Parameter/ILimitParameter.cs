namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in limit parameter
    /// </summary>
    public interface ILimitParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Value of limit
        /// </summary>
        long Value { get; set; }
    }
}
