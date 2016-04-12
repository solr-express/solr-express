using System.Collections.Generic;

namespace SolrExpress.Core.Result
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
