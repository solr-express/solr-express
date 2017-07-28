namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use write type parameter
    /// </summary>
    public interface IDefaultFieldParameter<TDocument> : ISearchParameter, ISearchItemFieldExpression<TDocument>
        where TDocument : Document
    {
    }
}
