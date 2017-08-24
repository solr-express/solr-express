namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Write type parameter
    /// </summary>
    public interface IDefaultFieldParameter<TDocument> : ISearchParameter, ISearchItemFieldExpression<TDocument>
        where TDocument : Document
    {
    }
}
