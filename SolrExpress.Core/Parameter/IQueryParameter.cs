using SolrExpress.Core.ParameterValue;

namespace SolrExpress.Core.Parameter
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
    }
}
