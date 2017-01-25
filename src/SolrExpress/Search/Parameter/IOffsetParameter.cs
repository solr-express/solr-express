namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in offset parameter
    /// </summary>
    public interface IOffsetParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure value of offset
        /// </summary>
        /// <param name="value">Value of offset</param>
        IOffsetParameter<TDocument> Value(long value);
    }
}
