namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Sort parameter
    /// </summary>
    public interface ISortParameter<TDocument> : ISearchParameter, ISearchItemFieldExpression<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// True to ascendent order, otherwise false
        /// </summary>
        bool Ascendent { get; set; }
    }
}
