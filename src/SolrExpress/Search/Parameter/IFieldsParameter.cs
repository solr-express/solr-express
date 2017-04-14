namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in fields parameter
    /// </summary>
    public interface IFieldsParameter<TDocument> : ISearchParameter, ISearchParameterFieldExpressions<TDocument>
        where TDocument : IDocument
    {
    }
}
