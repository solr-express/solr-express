namespace SolrExpress.Solr5.UnitTests
{
    public class SimpleTestDocument : Document
    {
        [SolrField("dummy")]
        public string Dummy { get; set; }
    }
}
