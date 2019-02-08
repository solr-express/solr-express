using System;

namespace SolrExpress.UnitTests
{
    public class TestDocumentNoAttributes : Document
    {
        public DateTime CreatedAt { get; set; }

        public GeoCoordinate Spatial { get; set; }

        public string Dummy { get; set; }
    }
}
