using SolrExpress.Query;

namespace SolrExpress.Solr5.Tests
{
    public class TestDocument : IDocument
    {
        public string Id { get; set; }

        public decimal Score { get; set; }
    }
}
