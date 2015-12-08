using SolrExpress.Core.Entity;
using System.Collections.Generic;

namespace SolrExpress.Core.Builder
{
    public interface IFacetQueryResultBuilder<TDocument> : IResultBuilder
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        Dictionary<string, long> Data { get; }
    }
}
