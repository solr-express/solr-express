namespace SolrExpress.Core.Result
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
