namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in sort parameter
    /// </summary>
    public interface IRandomSortParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
    }
}
