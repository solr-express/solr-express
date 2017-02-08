using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    public interface IFacetQueryResult<TDocument> : ISearchResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        IDictionary<string, long> Data { get; set; }
    }
}
