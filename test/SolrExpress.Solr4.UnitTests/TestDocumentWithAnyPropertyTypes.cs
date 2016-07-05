using SolrExpress.Core;
using System;

namespace SolrExpress.Solr4.UnitTests
{
    public class TestDocumentWithAnyPropertyTypes : IDocument
    {
        public int PropInteger { get; set; }

        public long PropLong { get; set; }

        public float PropFloat { get; set; }

        public double PropDouble { get; set; }

        public decimal PropDecimal { get; set; }

        public DateTime PropDateTime { get; set; }

        public string PropString { get; set; }
    }
}
