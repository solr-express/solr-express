using SolrExpress.Core;

namespace SolrExpress.Core.UnitTests
{
    public class TestDocumentSimple : IDocument
    {
        public string Id { get; set; }

        public string Text { get; set; }
    }
}
