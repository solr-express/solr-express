using System;

namespace SolrExpress.UnitTests
{
    public class TestDocument : Document
    {
        [SolrField("_created_at_")]
        public DateTime CreatedAt { get; set; }

        [SolrField("_spatial_")]
        public GeoCoordinate Spatial { get; set; }

        [SolrField("_dummy_")]
        public string Dummy { get; set; }

        [SolrField("indexed_false")]
        public long IndexedFalse { get; set; }
    }
}
