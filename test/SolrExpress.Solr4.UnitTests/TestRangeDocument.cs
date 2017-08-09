using System;

namespace SolrExpress.Solr4.UnitTests
{
    public class TestRangeDocument : Document
    {
        [SolrField("range1")]
        public decimal Range1 { get; set; }

        [SolrField("range2")]
        public int Range2 { get; set; }

        [SolrField("range3")]
        public int Range3 { get; set; }

        [SolrField("range4")]
        public DateTime Range4 { get; set; }
    }
}
