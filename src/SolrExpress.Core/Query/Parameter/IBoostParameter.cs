using SolrExpress.Core.Query.ParameterValue;

namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use boost parameter
    /// </summary>
    public interface IBoostParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        /// <param name="boostFunctionType">Boost type used in calculation</param>
        IBoostParameter<TDocument> Configure(IQueryParameterValue query, BoostFunctionType boostFunctionType);

        /// <summary>
        /// Query used to make boost
        /// </summary>
        IQueryParameterValue Query { get; }

        /// <summary>
        /// Boost type used in calculation
        /// </summary>
        BoostFunctionType BoostFunctionType { get; }
    }
}
