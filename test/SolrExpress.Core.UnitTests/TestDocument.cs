namespace SolrExpress.Core.UnitTests
{
    public class TestDocument : IDocument
    {
        [SolrField("_id_")]
        public string Id { get; set; }

        [SolrField("_score_")]
        public decimal Score { get; set; }

        [SolrField("_spatial_")]
        public GeoCoordinate Spatial { get; set; }

        [SolrField("_dummy_")]
        public string Dummy { get; set; }
    }
}
