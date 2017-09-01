namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Facet field parameter
    /// </summary>
    public interface IFacetFieldParameter<TDocument> : IFacetParameter<TDocument>, ISearchItemFieldExpression<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Method type to how to facet the field
        /// </summary>
        FacetMethodType? MethodType { get; set; }
    }
}
