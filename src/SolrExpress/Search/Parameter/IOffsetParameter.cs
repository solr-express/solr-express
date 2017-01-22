namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in offset parameter
    /// </summary>
    public interface IOffsetParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Value of limit
        /// </summary>
        long Value { get; set;}
    }
}
