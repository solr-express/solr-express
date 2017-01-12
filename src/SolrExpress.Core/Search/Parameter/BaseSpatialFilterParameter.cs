using SolrExpress.Core.Search.ParameterValue;
using SolrExpress.Core.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseSpatialFilterParameter<TDocument> : ISpatialFilterParameter<TDocument>, IValidation
        where TDocument : IDocument
    {
        protected IExpressionBuilder<TDocument> _expressionBuilder;

        public BaseSpatialFilterParameter(IExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; private set; }

        /// <summary>
        /// Function used in the spatial filter
        /// </summary>
        public SolrSpatialFunctionType FunctionType { get; private set; }

        /// <summary>
        /// Center point to spatial filter
        /// </summary>
        public GeoCoordinate CenterPoint { get; private set; }

        /// <summary>
        /// Distance from the center point
        /// </summary>
        public decimal Distance { get; private set; }
        
        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            isValid = true;
            errorMessage = string.Empty;

            var solrFieldAttribute = this._expressionBuilder.GetSolrFieldAttributeFromPropertyInfo(this.Expression);

            if (solrFieldAttribute?.Indexed ?? true)
            {
                return;
            }

            isValid = false;
            errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAQueryException;
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        public ISpatialFilterParameter<TDocument> Configure(Expression<Func<TDocument, object>> expression, SolrSpatialFunctionType functionType, GeoCoordinate centerPoint, decimal distance)
        {
            Checker.IsNull(expression);

            this.FunctionType = functionType;
            this.Expression = expression;
            this.CenterPoint = centerPoint;
            this.Distance = distance;

            return this;
        }
    }
}
