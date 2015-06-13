using Newtonsoft.Json.Linq;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class SortParameter<T> : IParameter<JObject>
        where T : IDocument
    {
        private readonly string _value;

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public SortParameter(Expression<Func<T, object>> expression, bool ascendent)
        {
            var fieldName = UtilHelper.GetFieldNameFromExpression(expression);

            this._value = string.Concat(fieldName, " ", ascendent ? "asc" : "desc");
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
            var jArray = (JArray)jObject["sort"] ?? new JArray();

            jArray.Add(this._value);

            jObject["sort"] = jArray;
        }
    }
}
