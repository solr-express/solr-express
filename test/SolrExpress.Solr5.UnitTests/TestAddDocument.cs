namespace SolrExpress.Solr5.UnitTests
{
    public class TestAddDocument : Document
    {
        [SolrField("dummy")]
        public string Dummy { get; set; }

        [SolrField("dummy2")]
        public string Dummy2 { get; set; }
    }
}
