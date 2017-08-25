namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Sort parameter
    /// </summary>
    public interface ISortRandomlyParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
    }
}
