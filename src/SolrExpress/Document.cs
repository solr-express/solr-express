namespace SolrExpress
{
    /// <summary>
    /// Base class to use in all SOLR documents
    /// </summary>
    public abstract class Document
    {
        [SolrField("id")]
        public string Id { get; set; }

        [SolrField("score", IsMagicField = true)]
        public string Score { get; set; }
    }
}
