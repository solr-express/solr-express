using SolrExpress.Core.Query.ParameterValue;

namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use in query parameter
    /// </summary>
    public interface IQueryParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        IQueryParameter<TDocument> Configure(IQueryParameterValue value);

        /// <summary>
        /// Parameter to include in the query
        /// </summary>
        IQueryParameterValue Value { get; }
    }
}
