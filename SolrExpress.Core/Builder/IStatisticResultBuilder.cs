using SolrExpress.Core.Entity;

namespace SolrExpress.Core.Builder
{
    public interface IStatisticResultBuilder<TDocument> : IResultBuilder
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        Statistic Data { get; }
    }
}
