using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    public interface IDocumentResult<TDocument> : ISearchResult
         where TDocument : IDocument
    {
        /// <summary>
        /// Documents of search
        /// </summary>
        IEnumerable<TDocument> Data { get; set; }
    }
}
