using System.Collections.Generic;

namespace SolrExpress.Core.Result
{
    public interface IFacetFieldResult<TDocument> : IResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        List<FacetKeyValue<string>> Data { get; }
    }
}
