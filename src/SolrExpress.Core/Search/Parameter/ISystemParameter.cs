namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in internal use
    /// </summary>
    public interface ISystemParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        ISystemParameter<TDocument> Configure();
    }
}
