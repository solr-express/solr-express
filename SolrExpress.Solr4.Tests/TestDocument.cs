using SolrExpress.Core.Attribute;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr4.Tests
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
