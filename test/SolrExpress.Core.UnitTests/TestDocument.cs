namespace SolrExpress.Core.UnitTests
{
    public class TestDocument : IDocument
    {
        public string Id { get; set; }

        public decimal Score { get; set; }

        [SolrField("_spatial_")]
        public GeoCoordinate Spatial { get; set; }

        [SolrField("_dummy_")]
        public string Dummy { get; set; }
    }
}
