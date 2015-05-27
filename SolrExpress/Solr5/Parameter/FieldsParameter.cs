using Newtonsoft.Json.Linq;
using SolrExpress.Helper;
using SolrExpress.Query;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Parameter
{
    public class FieldsParameter<T> : IQueryParameter
        where T : IDocument
    {
        private string _value;

        public FieldsParameter()
        {

        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        public FieldsParameter(Expression<Func<T, object>> expression)
        {
            this._value = UtilHelper.GetPropertyNameFromExpression(expression);
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstance { get { return true; } }

        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParameterName { get { return "fields"; } }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jArray = (JArray)jObject[this.ParameterName] ?? new JArray();

            jArray.Add(this._value);

            jObject[this.ParameterName] = jArray;
        }

        public void Set(Expression<Func<T, object>> expression)
        {
        }
    }
}
