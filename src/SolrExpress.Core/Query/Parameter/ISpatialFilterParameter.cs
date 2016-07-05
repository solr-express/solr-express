using SolrExpress.Core.Query.ParameterValue;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use in spatial filter parameter
    /// </summary>
    public interface ISpatialFilterParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        ISpatialFilterParameter<TDocument> Configure(Expression<Func<TDocument, object>> expression, SolrSpatialFunctionType functionType, GeoCoordinate centerPoint, decimal distance);

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        Expression<Func<TDocument, object>> Expression { get; }

        /// <summary>
        /// Function used in the spatial filter
        /// </summary>
        SolrSpatialFunctionType FunctionType { get; }

        /// <summary>
        /// Center point to spatial filter
        /// </summary>
        GeoCoordinate CenterPoint { get; }

        /// <summary>
        /// Distance from the center point
        /// </summary>
        decimal Distance { get; }
    }
}