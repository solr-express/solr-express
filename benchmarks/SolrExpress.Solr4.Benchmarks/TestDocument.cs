using SolrExpress.Core;

namespace SolrExpress.Solr4.Benchmarks
{
    public class TestDocument : IDocument
    {
        public string Id { get; set; }

        public decimal Score { get; set; }
    }
}
