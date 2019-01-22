using SolrExpress.Search.Query;
using System.Collections.Generic;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Facet parameter
    /// </summary>
    public interface IFacetParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Services provider
        /// </summary>
        ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }

        /// <summary>
        /// List of subfacets
        /// </summary>
        IList<IFacetParameter<TDocument>> Facets { get; set; }

        /// <summary>
        /// Specify a filter or list of filters to be intersected with the incoming domain before faceting
        /// </summary>
        SearchQuery<TDocument> Filter { get; set; }

        /// <summary>
        /// Sort type of result of facet
        /// </summary>
        FacetSortType? SortType { get; set; }

        /// <summary>
        /// Minimum count of itens in facet's result
        /// </summary>
        int? Minimum { get; set; }

        /// <summary>
        /// Limit of itens in facet's result
        /// </summary>
        int? Limit { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        string[] Excludes { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        bool Equals(object obj);
    }
}
