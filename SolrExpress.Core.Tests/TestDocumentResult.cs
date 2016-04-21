using SolrExpress.Core.Result;
using System.Collections.Generic;

namespace SolrExpress.Core.Tests
{
    public class TestDocumentResult<TDocument> : IDocumentResult<TDocument>
        where TDocument : IDocument
    {
        public List<TDocument> Data { get; set; }
    }
}
