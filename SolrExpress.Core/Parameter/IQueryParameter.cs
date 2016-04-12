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
        /// Parameter to include in the query
        /// </summary>
        IQueryParameterValue Value { get; set; }
    }
}
