namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Query parser parameter
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
