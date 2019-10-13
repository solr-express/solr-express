namespace SolrExpress
{
    /// <summary>
    /// Base class to use in all SOLR documents
    /// </summary>
    public abstract class Document
    {
        public string Id { get; set; }

        public string Score { get; set; }
    }
}
