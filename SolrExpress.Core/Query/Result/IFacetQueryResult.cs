using System.Collections.Generic;

namespace SolrExpress.Core.Query.Result
{
    public interface IFacetQueryResult<TDocument> : IResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        Dictionary<string, long> Data { get; }
    }
}
