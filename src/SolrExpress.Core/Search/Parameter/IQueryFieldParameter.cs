namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in query field parameter
    /// </summary>
    public interface IQueryFieldParameter<TDocument> : ISearchParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Query used to make the query field</param>
        IQueryFieldParameter<TDocument> Configure(string expression);

        /// <summary>
        /// Query used to make the query field
        /// </summary>
        string Expression { get; }
    }
}
