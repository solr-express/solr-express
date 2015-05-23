using Newtonsoft.Json.Linq;
using SolrExpress.Helper;
using System;
using System.Linq.Expressions;

namespace SolrExpress.QueryBuilder.Parameter.Solr5
{
    public class QueryParameter<T> : IQueryParameter
        where T : IDocument
    {
        private string _value;

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public QueryParameter(string value)
        {
            this._value = value;
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the query</param>
        public QueryParameter(Expression<Func<T, object>> expression, string value)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this._value = string.Concat(fieldName, ":", value);
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstance { get { return false; } }

        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParameterName { get { return "query"; } }

        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject[this.ParameterName] = new JValue(_value);
        }
    }
}
