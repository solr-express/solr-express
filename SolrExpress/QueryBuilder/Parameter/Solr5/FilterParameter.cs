using Newtonsoft.Json.Linq;
using SolrExpress.Helper;
using System;
using System.Linq.Expressions;

namespace SolrExpress.QueryBuilder.Parameter.Solr5
{
    public class FilterParameter<T> : IQueryParameter
        where T : IDocument
    {
        private string _value;

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        public FilterParameter(Expression<Func<T, object>> expression, string value)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this._value = string.Concat(fieldName, ":", value);
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="from">From value in a range filter</param>
        public FilterParameter(Expression<Func<T, object>> expression, DateTime? from, DateTime? to)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this._value = string.Format(
                "{0}:[{1} TO {2}]",
                fieldName,
                from == null ? from.Value.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'") : "*",
                to == null ? to.Value.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'") : "*");
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="from">From value in a range filter</param>
        public FilterParameter(Expression<Func<T, object>> expression, int? from, int? to)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this._value = string.Format(
                "{0}:[{1} TO {2}]",
                fieldName,
                from == null ? from.Value.ToString("0") : "*",
                to == null ? to.Value.ToString("0") : "*");
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="from">From value in a range filter</param>
        public FilterParameter(Expression<Func<T, object>> expression, double? from, double? to)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this._value = string.Format(
                "{0}:[{1} TO {2}]",
                fieldName,
                from == null ? from.Value.ToString("0.#") : "*",
                to == null ? to.Value.ToString("0.#") : "*");
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="from">From value in a range filter</param>
        public FilterParameter(Expression<Func<T, object>> expression, decimal? from, decimal? to)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this._value = string.Format(
                "{0}:[{1} TO {2}]",
                fieldName,
                from == null ? from.Value.ToString("0.#") : "*",
                to == null ? to.Value.ToString("0.#") : "*");
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstance { get { return true; } }

        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParameterName { get { return "filter"; } }

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
