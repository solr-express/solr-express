namespace SolrExpress.Search.Query
{
    /// <summary>
    /// Solr queries container with TDocument link
    /// </summary>
    public class SearchQuery<TDocument> : SearchQuery
        where TDocument : IDocument
    {
    }
}
