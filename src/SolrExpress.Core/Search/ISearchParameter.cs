using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signatures to use in solr parameter
    /// </summary>
    public interface ISearchParameter<TDocument> : ISearchItem
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        bool AllowMultipleInstances { get; }

        /// <summary>
        /// Expressions builder
        /// </summary>
        IExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
    }

    /// <summary>
    /// Signatures to use in solr parameter
    /// </summary>
    public interface ISearchParameter<TDocument, TContainer> : ISearchParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Create the parameter
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        void Execute(TContainer container);
    }
}
