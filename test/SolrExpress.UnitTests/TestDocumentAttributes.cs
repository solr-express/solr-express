using System;

namespace SolrExpress.UnitTests
{
    public class TestDocumentAttributes : Document
    {
        [SolrField("_created_at_")]
        public DateTime CreatedAt { get; set; }

        [SolrField("_spatial_")]
        public GeoCoordinate Spatial { get; set; }

        [SolrField("_dummy_")]
        public string Dummy { get; set; }
    }
}
