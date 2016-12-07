using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signature to use in solr query parameter value
    /// </summary>
    public interface ISearchParameterValue<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        string Execute();

        /// <summary>
        /// Expressions builder
        /// </summary>
        IExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
    }
}
