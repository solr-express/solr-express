using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query;
using System;
using System.Diagnostics.Contracts;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FilterParameter : IParameter<JObject>
    {
        private readonly IQueryParameterValue _value;

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="value">Parameter value used to create the query</param>
        public FilterParameter(IQueryParameterValue value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            this._value = value;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jArray = (JArray)jObject["filter"] ?? new JArray();

            jArray.Add(this._value.Execute());

            jObject["filter"] = jArray;
        }
    }
}
