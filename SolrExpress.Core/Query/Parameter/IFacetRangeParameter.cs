using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use in facet range parameter
    /// </summary>
    public interface IFacetRangeParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetRangeParameter<TDocument> Configure(string aliasName, Expression<Func<TDocument, object>> expression, string gap, string start, string end, FacetSortType? sortType = null, params string[] excludes);

        /// <summary>
        /// Name of the alias added in the query
        /// </summary>
        string AliasName { get; }

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        Expression<Func<TDocument, object>> Expression { get; }

        /// <summary>
        /// Size of each range bucket to make the facet
        /// </summary>
        string Gap { get; }

        /// <summary>
        /// Lower bound to make the facet
        /// </summary>
        string Start { get; }

        /// <summary>
        /// Upper bound to make the facet
        /// </summary>
        string End { get; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        FacetSortType? SortType { get; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        string[] Excludes { get; }
    }
}