namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in query field parameter
    /// </summary>
    public interface IQueryFieldParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure query used to make query field
        /// </summary>
        /// <param name="expression">Query used to make query field</param>
        IQueryFieldParameter<TDocument> Expression(string expression);
    }
}
