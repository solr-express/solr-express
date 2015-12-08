using SolrExpress.Core.Entity;
using System.Collections.Generic;

namespace SolrExpress.Core.Builder
{
    public interface IFacetRangeResultBuilder<TDocument> : IResultBuilder
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        List<FacetKeyValue<FacetRange>> Data { get; }
    }
}
