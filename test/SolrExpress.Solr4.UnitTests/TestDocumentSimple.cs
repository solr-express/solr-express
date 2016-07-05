using SolrExpress.Core;

namespace SolrExpress.Solr4.UnitTests
{
    public class TestDocumentSimple : IDocument
    {
        public string Id { get; set; }

        public string Text { get; set; }
    }
}
