using SolrExpress.Core;

namespace SolrExpress.Solr4.UnitTests
{
    public class TestDocument : IDocument
    {
        public string Id { get; set; }

        public decimal Score { get; set; }

        public GeoCoordinate Spatial { get; set; }

        [SolrField("_dummy_")]
        public string Dummy { get; set; }
    }
}
