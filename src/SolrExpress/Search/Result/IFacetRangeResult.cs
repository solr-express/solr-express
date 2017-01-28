using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    public interface IFacetRangeResult<TDocument> : ISearchResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        IEnumerable<FacetKeyValue<FacetRange>> Data { get; }
    }
}
