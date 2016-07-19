using SolrExpress.Core;

namespace SolrExpress.Solr5.UnitTests
{
    public class TestDocument : IDocument
    {
        public string Id { get; set; }

        public decimal Score { get; set; }

        public GeoCoordinate Spatial { get; set; }

        [SolrField("_dummy_")]
        public string Dummy { get; set; }

        [SolrField("indexed_false", Indexed = false)]
        public long IndexedFalse { get; set; }
    }
}
