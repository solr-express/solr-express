using System.Collections.Generic;

namespace SolrExpress.Core.Search.Result
{
    public interface IFacetRangeResult<TDocument> : IResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        IEnumerable<FacetKeyValue<FacetRange>> Data { get; }
    }
}
