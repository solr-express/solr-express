namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in sort parameter
    /// </summary>
    public interface ISortRandomlyParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
    }
}
