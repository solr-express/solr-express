using Newtonsoft.Json.Linq;
using SolrExpress.Helper;
using System;
using System.Linq.Expressions;

namespace SolrExpress.QueryBuilder.Parameter
{
    public class SortParameter<T> : IQueryParameter
        where T : IDocument
    {
        private string _value;

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public SortParameter(Expression<Func<T, object>> expression, bool ascendent)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this._value = string.Concat(fieldName, " ", ascendent ? "asc" : "desc");
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstance { get { return true; } }

        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParameterName { get { return "sort"; } }

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
    }
}
