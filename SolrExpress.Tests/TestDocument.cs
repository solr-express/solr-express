using SolrExpress.QueryBuilder;

namespace SolrExpress.Tests
{
    internal class TestDocument : IDocument
    {
        public int Id { get; set; }

        public decimal Score { get; set; }
    }
}
