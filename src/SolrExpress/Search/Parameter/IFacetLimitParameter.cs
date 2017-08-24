namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Facet limit parameter
    /// </summary>
    public interface IFacetLimitParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Value of limit
        /// </summary>
        long Value { get; set; }
    }
}