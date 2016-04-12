using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use in facet range parameter
    /// </summary>
    public interface IFacetRangeParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Name of the alias added in the query
        /// </summary>
        string AliasName { get; set; }

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        string End { get; set; }

        /// <summary>
        /// Size of each range bucket to make the facet
        /// </summary>
        Expression<Func<TDocument, object>> Expression { get; set; }

        /// <summary>
        /// Lower bound to make the facet
        /// </summary>
        string Gap { get; set; }

        /// <summary>
        /// Upper bound to make the facet
        /// </summary>
        SolrFacetSortType? SortType { get; set; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        string Start { get; set; }
    }
}
