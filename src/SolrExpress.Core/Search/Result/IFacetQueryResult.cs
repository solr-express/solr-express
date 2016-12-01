using System.Collections.Generic;

namespace SolrExpress.Core.Search.Result
{
    public interface IFacetQueryResult<TDocument> : IResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        IDictionary<string, long> Data { get; }

        /// <summary>
        /// Tag
        /// </summary>
        object Tag { get; set; }
    }
}
