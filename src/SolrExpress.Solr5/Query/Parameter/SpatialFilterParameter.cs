using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Query.Parameter
{
    public sealed class SpatialFilterParameter<TDocument> : ISpatialFilterParameter<TDocument>, IParameter<JObject>, IValidation
        where TDocument : IDocument
    {
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
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var fieldName = this.Expression.GetFieldNameFromExpression();

            var jObj = (JObject)jObject["params"] ?? new JObject();

            var formule = this.FunctionType.GetSolrSpatialFormule(
                fieldName,
                this.CenterPoint,
                this.Distance);

            jObj.Add(new JProperty("fq", formule));

            jObject["params"] = jObj;
        }

        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            isValid = true;
            errorMessage = string.Empty;

            var solrFieldAttribute = this.Expression.GetSolrFieldAttributeFromPropertyInfo();

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
