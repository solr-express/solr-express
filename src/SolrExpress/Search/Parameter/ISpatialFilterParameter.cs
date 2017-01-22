using SolrExpress.Core.Search;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in spatial filter parameter
    /// </summary>
    public interface ISpatialFilterParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Expression used to find property name
        /// </summary>
        Expression<Func<TDocument, object>> FieldExpression { get; set;}

        /// <summary>
        /// Function used in spatial filter
        /// </summary>
        SpatialFunctionType FunctionType { get; set;}

        /// <summary>
        /// Center point to spatial filter
        /// </summary>
        GeoCoordinate CenterPoint { get; set;}

        /// <summary>
        /// Distance from center point
        /// </summary>
        decimal Distance { get; set;}
    }
}