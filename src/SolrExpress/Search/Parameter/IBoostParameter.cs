namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use boost parameter
    /// </summary>
    public interface IBoostParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Query used to make boost
        /// </summary>
        ISearchQuery<TDocument> Query { get; set;}

        /// <summary>
        /// Boost type used in calculation
        /// </summary>
        BoostFunctionType BoostFunctionType { get; set;}
    }
}
