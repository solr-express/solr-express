using Newtonsoft.Json.Linq;
using SolrExpress.Enumerator;
using SolrExpress.Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.QueryBuilder.Parameter.Solr5
{
    public class FacetQueryParameter<T> : IQueryParameter
        where T : IDocument
    {
        private JProperty _value;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="sortAscending">Sort ascending the result of the facet</param>
        public FacetQueryParameter(Expression<Func<T, object>> expression, string query, SolrFacetSortType? sortType, bool? sortAscending)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            var array = new List<JProperty>();
            array.Add(new JProperty("q", fieldName));

            if (sortType.HasValue && sortAscending.HasValue)
            {
                var typeName = sortType.Value == SolrFacetSortType.Name ? "index" : "count";
                var sortName = sortAscending.Value ? "asc" : "desc";

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            this._value = new JProperty(fieldName, new JObject(new JProperty("query", new JObject(array.ToArray()))));
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstance { get { return true; } }

        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParameterName { get { return "facet"; } }

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
