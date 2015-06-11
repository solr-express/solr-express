using System;
using System.Globalization;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class SpatialFilterParameter<TDocument> : IParameter<JObject>
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
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(this._expression);

            var formule = string.Format("{{!{0} sfield={1}}}", this._functionType.ToString().ToLower(), fieldName);

            var jObj = (JObject)jObject["params"] ?? new JObject();

            jObj.Add(new JProperty("fq", formule));
            jObj.Add(new JProperty("pt", this._centerPoint.ToString()));
            jObj.Add(new JProperty("d", this._distance.ToString("0.#", CultureInfo.InvariantCulture)));

            jObject["params"] = jObj;
        }
    }
}
