namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signatures to use in solr parameter
    /// </summary>
    public interface ISearchParameterExecute<TContainer>
    {
        /// <summary>
        /// Create the parameter
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        void Execute(TContainer container);
    }
}
