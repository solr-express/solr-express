namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in fields parameter
    /// </summary>
    public interface IFieldsParameter<TDocument> : ISearchParameter, ISearchItemFieldExpressions<TDocument>
        where TDocument : IDocument
    {
    }
}
