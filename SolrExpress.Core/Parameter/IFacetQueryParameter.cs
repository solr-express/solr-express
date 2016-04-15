using SolrExpress.Core.ParameterValue;

namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use in facet query parameter
    /// </summary>
    public interface IFacetQueryParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetQueryParameter<TDocument> Configure(string aliasName, IQueryParameterValue query, SolrFacetSortType? sortType = null, params string[] excludes);
    }
}
