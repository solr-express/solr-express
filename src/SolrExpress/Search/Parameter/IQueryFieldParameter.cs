namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Query field parameter
    /// </summary>
    public interface IQueryFieldParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Query used to make query field
        /// </summary>
        string Expression { get; set; }
    }
}
