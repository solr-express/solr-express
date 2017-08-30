namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Facet field parameter
    /// </summary>
    public interface IFacetFieldParameter<TDocument> : IFacetParameter<TDocument>, ISearchItemFieldExpression<TDocument>
        where TDocument : Document
    {
    }
}
