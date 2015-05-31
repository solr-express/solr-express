using SolrExpress.Query;

namespace SolrExpress.Solr4.Tests
{
    public class TestDocument : IDocument
    {
        public string Id { get; set; }

        public decimal Score { get; set; }
    }
}
