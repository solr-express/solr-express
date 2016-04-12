namespace SolrExpress.Core.Result
{
    public interface IStatisticResult<TDocument> : IResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        Statistic Data { get; }
    }
}
