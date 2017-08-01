namespace SolrExpress.UnitTests
{
    public class TestDocumentSimple : Document
    {
        [SolrField("text")]
        public string Text { get; set; }
    }
}
