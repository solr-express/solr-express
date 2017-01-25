namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in limit parameter
    /// </summary>
    public interface ILimitParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure value of limit
        /// </summary>
        /// <param name="value">Value of limit</param>
        ILimitParameter<TDocument> Value(long value);
    }
}
