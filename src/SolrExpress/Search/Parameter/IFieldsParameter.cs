namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Ffields parameter
    /// </summary>
    public interface IFieldsParameter<TDocument> : ISearchParameter, ISearchItemFieldExpressions<TDocument>
        where TDocument : Document
    {
    }
}
