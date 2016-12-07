using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search.ParameterValue
{
    /// <summary>
    /// Query all value parameter
    /// </summary>
    public sealed class QueryAll<TDocument> : ISearchParameterValue<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute() => "*:*";

        /// <summary>
        /// Expressions builder
        /// </summary>
        public IExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
    }
}
