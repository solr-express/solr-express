using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    public interface IFacetsResult<TDocument> : ISearchResult
        where TDocument : Document
    {
        /// <summary>
        /// Facet data
        /// </summary>
        IEnumerable<FacetKeyValue> Data { get; set; }
    }
}
