using Newtonsoft.Json.Linq;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FilterParameter : IFilterParameter, IParameter<JObject>
    {
        private readonly IQueryParameterValue _value;
        private readonly string _tagName;

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="value">Parameter value used to create the query</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public FilterParameter(IQueryParameterValue value, string tagName = null)
        {
            ThrowHelper<ArgumentNullException>.If(value == null);

            this._value = value;
            this._tagName = tagName;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jArray = (JArray)jObject["filter"] ?? new JArray();

            jArray.Add(UtilHelper.GetSolrFilterWithTag(this._tagName, this._value.Execute()));

            jObject["filter"] = jArray;
        }
    }
}
