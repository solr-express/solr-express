using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;

namespace SolrExpress.Solr5.Query.Parameter
{
    public sealed class FilterParameter<TDocument> : IFilterParameter<TDocument>, IParameter<JObject>
        where TDocument : IDocument
    {
        private IQueryParameterValue _value;
        private string _tagName;

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

            jArray.Add(this._value.Execute().GetSolrFilterWithTag(this._tagName));

            jObject["filter"] = jArray;
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public IFilterParameter<TDocument> Configure(IQueryParameterValue value, string tagName = null)
        {
            Checker.IsNull(value);

            this._value = value;
            this._tagName = tagName;

            return this;
        }
    }
}
