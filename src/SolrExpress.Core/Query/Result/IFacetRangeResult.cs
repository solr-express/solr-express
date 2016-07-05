using System.Collections.Generic;

namespace SolrExpress.Core.Query.Result
{
    public interface IFacetRangeResult<TDocument> : IResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        List<FacetKeyValue<FacetRange>> Data { get; }
    }
}
