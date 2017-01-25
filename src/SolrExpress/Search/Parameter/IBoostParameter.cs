namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use boost parameter
    /// </summary>
    public interface IBoostParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure query used to make boost
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        IBoostParameter<TDocument> Query(ISearchQuery<TDocument> query);

        /// <summary>
        /// Configure oost type used in calculation
        /// </summary>
        /// <param name="boostFunctionType">Boost type used in calculation</param>
        IBoostParameter<TDocument> BoostFunctionType(BoostFunctionType boostFunctionType);
    }
}
