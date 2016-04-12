using System.Collections.Generic;

namespace SolrExpress.Core.Result
{
    public interface IDocumentBuilder<TDocument> : IResultBuilder
        where TDocument : IDocument
    {
        /// <summary>
        /// Documents of the search
        /// </summary>
        List<TDocument> Data { get; }
    }
}
