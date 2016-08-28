using SolrExpress.Core.Search.Result;
using System.Collections.Generic;

namespace SolrExpress.Core.UnitTests
{
    public class TestDocumentResult<TDocument> : IDocumentResult<TDocument>
        where TDocument : IDocument
    {
        public IEnumerable<TDocument> Data { get; set; }
    }
}
