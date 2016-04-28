using SolrExpress.Core;

namespace SolrExpress.Solr5.UnitTests
{
    public class TestDocumentSimple : IDocument
    {
        public string Id { get; set; }

        public string Text { get; set; }
    }
}
