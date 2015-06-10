using Newtonsoft.Json.Linq;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FieldsParameter<T> : IParameter
        where T : IDocument
    {
        private readonly string _value;

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
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jArray = (JArray)jObject["fields"] ?? new JArray();

            jArray.Add(this._value);

            jObject["fields"] = jArray;
        }

        public void Set(Expression<Func<T, object>> expression)
        {
        }
    }
}
