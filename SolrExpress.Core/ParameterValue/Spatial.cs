using SolrExpress.Core.Extension.Internal;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.ParameterValue
{
    /// <summary>
    /// Result spatial value
    /// </summary>
    public sealed class Spatial<TDocument> : IQueryParameterValue
        where TDocument : IDocument
    {
        private readonly SolrSpatialFunctionType _functionType;
        private readonly Expression<Func<TDocument, object>> _expression;
        private readonly GeoCoordinate _centerPoint;
        private readonly decimal _distance;

        /// <summary>
        /// Create a spatial filter parameter
        /// </summary>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        public Spatial(SolrSpatialFunctionType functionType, Expression<Func<TDocument, object>> expression, GeoCoordinate centerPoint, decimal distance)
        {
            Checker.IsNull(expression);

            this._functionType = functionType;
            this._expression = expression;
            this._centerPoint = centerPoint;
            this._distance = distance;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute()
        {
            var fieldName = this._expression.GetFieldNameFromExpression();

            return this._functionType.GetSolrSpatialFormule(
                fieldName,
                this._centerPoint,
                this._distance);
        }
    }
}
