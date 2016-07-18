using SolrExpress.Core;
using System;

namespace SolrExpress.Solr4.UnitTests
{
    public class TestDocument : IDocument
    {
        public string Id { get; set; }

        public decimal Score { get; set; }

        public DateTime CreatedAt { get; set; }

        public GeoCoordinate Spatial { get; set; }

        [SolrField("_dummy_")]
        public string Dummy { get; set; }
    }
}
