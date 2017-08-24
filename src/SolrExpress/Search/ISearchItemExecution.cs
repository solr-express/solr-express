namespace SolrExpress.Search
{
    /// <summary>
    /// Solr item execution
    /// </summary>
    public interface ISearchItemExecution<in TContainer>
    {
        /// <summary>
        /// Execute search item
        /// </summary>
        void Execute();

        /// <summary>
        /// Add search item result in container
        /// </summary>
        /// <param name="container">Container of items to request to Solr</param>
        void AddResultInContainer(TContainer container);
    }
}
