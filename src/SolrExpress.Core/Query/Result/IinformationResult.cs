namespace SolrExpress.Core.Query.Result
{
    public interface IInformationResult<TDocument> : IResult
        where TDocument : IDocument
    {
        /// <summary>
        /// Facet data
        /// </summary>
        Information Data { get; }
    }
}
