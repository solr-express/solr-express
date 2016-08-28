using System.Collections.Generic;

namespace SolrExpress.Core.Search.Result
{
    public interface IFacetFieldResult<TDocument> : IResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        IEnumerable<FacetKeyValue<string>> Data { get; }
    }
}
