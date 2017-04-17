namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in sort parameter
    /// </summary>
    public interface ISortParameter<TDocument> : ISearchParameter, ISearchItemFieldExpression<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// True to ascendent order, otherwise false
        /// </summary>
        bool Ascendent { get; set; }
    }
}
