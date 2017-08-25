using System;

namespace SolrExpress.Solr5.UnitTests
{
    public class TestFacetDocument : Document
    {
        [SolrField("range1")]
        public decimal Range1 { get; set; }

        [SolrField("range2")]
        public int Range2 { get; set; }

        [SolrField("range3")]
        public int Range3 { get; set; }

        [SolrField("range4")]
        public DateTime Range4 { get; set; }

        [SolrField("field1")]
        public decimal Field1 { get; set; }

        [SolrField("field2")]
        public int Field2 { get; set; }

        [SolrField("field3")]
        public int Field3 { get; set; }
    }
}
