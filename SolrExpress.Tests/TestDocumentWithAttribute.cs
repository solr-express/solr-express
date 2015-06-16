using SolrExpress.Core.Attribute;
using SolrExpress.Core.Query;

namespace SolrExpress.Tests
{
    public class TestDocumentWithAttribute : IDocument
    {
        [SolrFieldAttribute("Indexed", Indexed = true)]
        public string Indexed { get; set; }

        [SolrFieldAttribute("NotIndexed", Indexed = false)]
        public string NotIndexed { get; set; }

        [SolrFieldAttribute("Stored", Stored = true)]
        public string Stored { get; set; }

        [SolrFieldAttribute("NotStored", Stored = false)]
        public string NotStored { get; set; }
    }
}
