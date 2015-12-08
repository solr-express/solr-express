using SolrExpress.Core.Entity;
using System.Collections.Generic;

namespace SolrExpress.Core.Builder
{
    public interface IFacetFieldResultBuilder<TDocument> : IResultBuilder
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        List<FacetKeyValue<string>> Data { get; }
    }
}
