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
        /// Configure expression used to find field name
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        ISpatialFilterParameter<TDocument> FieldExpression(Expression<Func<TDocument, object>> fieldExpression);

        /// <summary>
        /// Configure function used in spatial filter
        /// </summary>
        /// <param name="functionType">Function used in spatial filter</param>
        ISpatialFilterParameter<TDocument> FunctionType(SpatialFunctionType functionType);

        /// <summary>
        /// Configure center point to spatial filter
        /// </summary>
        /// <param name="centerPoint">Center point to spatial filter</param>
        ISpatialFilterParameter<TDocument> CenterPoint(GeoCoordinate centerPoint);

        /// <summary>
        /// Configure distance from center point
        /// </summary>
        /// <param name="distance">Distance from center point</param>
        ISpatialFilterParameter<TDocument> Distance(decimal distance);
    }
}