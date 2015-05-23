using Newtonsoft.Json.Linq;
using SolrExpress.Enumerator;
using System.Collections.Generic;

namespace SolrExpress.QueryBuilder.Parameter
{
    public class FieldsParameter : IQueryParameter
    {
        private JArray _jArray = new JArray();

        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParameterName { get { return "fields"; } }

        /// <summary>
        /// Execute the creation of the parameter "fields"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject[this.ParameterName] = this._jArray;
        }

        /// <summary>
        /// Add the value to the parameter fields
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        /// <returns>It self</returns>
        public FieldsParameter Add(string value)
        {
            this._jArray.Add(value);

            return this;
        }
    }
}
