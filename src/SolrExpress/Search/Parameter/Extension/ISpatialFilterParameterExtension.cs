using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure in spatial filter parameter
    /// </summary>
    public static class ISpatialFilterParameterExtension

    {
        /// <summary>
        /// Configure expression used to find field name
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        public static ISpatialFilterParameter<TDocument> FieldExpression<TDocument>(this ISpatialFilterParameter<TDocument> parameter, Expression<Func<TDocument, object>> fieldExpression)
            where TDocument : Document
        {
            parameter.FieldExpression = fieldExpression;

            return parameter;
        }

        /// <summary>
        /// Configure function used in spatial filter
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="functionType">Function used in spatial filter</param>
        public static ISpatialFilterParameter<TDocument> FunctionType<TDocument>(this ISpatialFilterParameter<TDocument> parameter, SpatialFunctionType functionType)
            where TDocument : Document
        {
            parameter.FunctionType = functionType;

            return parameter;
        }

        /// <summary>
        /// Configure center point to spatial filter
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        public static ISpatialFilterParameter<TDocument> CenterPoint<TDocument>(this ISpatialFilterParameter<TDocument> parameter, GeoCoordinate centerPoint)
            where TDocument : Document
        {
            parameter.CenterPoint = centerPoint;

            return parameter;
        }

        /// <summary>
        /// Configure distance from center point
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="distance">Distance from center point</param>
        public static ISpatialFilterParameter<TDocument> Distance<TDocument>(this ISpatialFilterParameter<TDocument> parameter, decimal distance)
            where TDocument : Document
        {
            parameter.Distance = distance;

            return parameter;
        }
    }
}