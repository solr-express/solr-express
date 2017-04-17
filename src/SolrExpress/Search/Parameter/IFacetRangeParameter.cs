using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet range parameter
    /// </summary>
    public interface IFacetRangeParameter<TDocument> : ISearchParameter, ISearchItemFieldExpression<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Name of alias added in query
        /// </summary>
        string AliasName { get; set; }
        
        /// <summary>
        /// Size of each range bucket to make facet
        /// </summary>
        string Gap { get; set; }

        /// <summary>
        /// Lower bound to make facet
        /// </summary>
        string Start { get; set; }

        /// <summary>
        /// Upper bound to make facet
        /// </summary>
        string End { get; set; }

        /// <summary>
        /// Minimum count of itens in facet's result
        /// </summary>
        int? Minimum { get; set; }

        /// <summary>
        /// Limit of itens in facet's result
        /// </summary>
        int? Limit { get; set; }

        /// <summary>
        /// Sort type of result of facet
        /// </summary>
        FacetSortType? SortType { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        string[] Excludes { get; set; }

        /// <summary>
        /// Counts should also be computed for all records with field values lower then lower bound of the first range
        /// </summary>
        bool CountBefore { get; set; }

        /// <summary>
        /// Counts should also be computed for all records with field values greater then the upper bound of the last range
        /// </summary>
        bool CountAfter { get; set; }
    }
}