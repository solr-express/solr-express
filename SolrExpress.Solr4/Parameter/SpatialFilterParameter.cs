using SolrExpress.Core.Entity;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class SpatialFilterParameter<TDocument> : IParameter<List<string>>, IValidation
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
        public SpatialFilterParameter(SolrSpatialFunctionType functionType, Expression<Func<TDocument, object>> expression, GeoCoordinate centerPoint, decimal distance)
        {
            ThrowHelper<ArgumentNullException>.If(expression == null);

            this._functionType = functionType;
            this._expression = expression;
            this._centerPoint = centerPoint;
            this._distance = distance;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return false; } }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            var fieldName = UtilHelper.GetFieldNameFromExpression(this._expression);

            var formule = string.Format("{{!{0} sfield={1}}}", this._functionType.ToString().ToLower(), fieldName);

            container.Add(string.Concat("fq=", formule));
            container.Add(string.Concat("pt=", this._centerPoint.ToString()));
            container.Add(string.Concat("d=", this._distance.ToString("0.#", CultureInfo.InvariantCulture)));
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

            var solrFieldAttribute = UtilHelper.GetSolrFieldAttributeFromPropertyInfo(this._expression);
            
            if (solrFieldAttribute != null && !solrFieldAttribute.Indexed)
            {
                isValid = false;
                errorMessage = "A field must be \"indexed=true\" to be used in a query";
            }
        }
    }
}
