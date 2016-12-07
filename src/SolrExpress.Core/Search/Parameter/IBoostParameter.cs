namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use boost parameter
    /// </summary>
    public interface IBoostParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        /// <param name="boostFunctionType">Boost type used in calculation</param>
        IBoostParameter<TDocument> Configure(ISearchParameterValue<TDocument> query, BoostFunctionType boostFunctionType);

        /// <summary>
        /// Query used to make boost
        /// </summary>
        ISearchParameterValue<TDocument> Query { get; }

        /// <summary>
        /// Boost type used in calculation
        /// </summary>
        BoostFunctionType BoostFunctionType { get; }
    }
}
