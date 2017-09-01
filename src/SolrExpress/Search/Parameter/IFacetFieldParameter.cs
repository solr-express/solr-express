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

        /// <summary>
        /// Prefix to only produce buckets for terms starting with the specified value
        /// </summary>
        string Prefix { get; set; }
    }
}
