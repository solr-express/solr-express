using System.Collections.Generic;

namespace SolrExpress.Core.Search.Result
{
    public interface IDocumentResult<TDocument> : IResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Documents of the search
        /// </summary>
        IEnumerable<TDocument> Data { get; }
    }
}
