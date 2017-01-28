using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    public interface IFacetFieldResult<TDocument> : ISearchResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        IEnumerable<FacetKeyValue<string>> Data { get; }
    }
}
