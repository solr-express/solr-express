using SolrExpress.Core.ParameterValue;
using System.Collections.Generic;

namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use in facet query parameter
    /// </summary>
    public interface IFacetQueryParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Name of the alias added in the query
        /// </summary>
        string AliasName { get; set; }

        /// <summary>
        /// Query used to make the facet
        /// </summary>
        IQueryParameterValue Query { get; set; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        SolrFacetSortType? SortType { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        List<string> Excludes { get; set; }
    }
}
