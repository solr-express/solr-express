using SolrExpress.Core.ParameterValue;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use in facet spatial parameter
    /// </summary>
    public interface IFacetSpatialParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Name of the alias added in the query
        /// </summary>
        string AliasName { get; set; }

        /// <summary>
        /// Function used in the spatial filter
        /// </summary>
        SolrSpatialFunctionType FunctionType { get; set; }

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        Expression<Func<TDocument, object>> Expression { get; set; }

        /// <summary>
        /// Center point to spatial filter
        /// </summary>
        GeoCoordinate CenterPoint { get; set; }

        /// <summary>
        /// Distance from the center point
        /// </summary>
        decimal Distance { get; set; }

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
