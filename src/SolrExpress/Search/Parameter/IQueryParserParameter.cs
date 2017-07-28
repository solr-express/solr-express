namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use query parser parameter
    /// </summary>
    public interface IQueryParserParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Query parser type used in SOLR's search
        /// </summary>
        QueryParserType Value { get; set; }
    }
}
