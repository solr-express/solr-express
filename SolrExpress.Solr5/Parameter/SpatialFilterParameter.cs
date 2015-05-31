using Newtonsoft.Json.Linq;
using SolrExpress.Helper;
using SolrExpress.Query;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class SpatialFilterParameter<T> : IQueryParameter
            where T : IDocument
    {
        private readonly JProperty _value;

        /// <summary>
        /// Create a spatial filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public SpatialFilterParameter(Expression<Func<T, object>> expression, GeoCoordinate? from, GeoCoordinate? to)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            var query = string.Format(
                "{0}:[{1} TO {2}]",
                fieldName,
                from != null ? from.Value.ToString() : "*",
                to != null ? to.Value.ToString() : "*");

            this._value = new JProperty("fq", query);
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return false; } }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();

            jObj.Add(this._value);

            jObject["params"] = jObj;
        }
    }
}
