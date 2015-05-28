using Newtonsoft.Json.Linq;
using SolrExpress.Enumerator;
using SolrExpress.Helper;
using SolrExpress.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FacetRangeParameter<T> : IQueryParameter
      where T : IDocument
    {
        private readonly JProperty _value;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="sortAscending">Sort ascending the result of the facet</param>
        public FacetRangeParameter(Expression<Func<T, object>> expression, string aliasName, string gap = null, string start = null, string end = null, SolrFacetSortType? sortType = null, bool? sortAscending = null)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            var array = new List<JProperty>
            {
                new JProperty("field", fieldName)
            };

            if (!string.IsNullOrWhiteSpace(gap))
            {
                array.Add(new JProperty("gap", gap));
            }
            if (!string.IsNullOrWhiteSpace(start))
            {
                array.Add(new JProperty("start", start));
            }
            if (!string.IsNullOrWhiteSpace(end))
            {
                array.Add(new JProperty("end", end));
            }

            array.Add(new JProperty("other", new JArray("before", "after")));

            if (sortType.HasValue && sortAscending.HasValue)
            {
                var typeName = sortType.Value == SolrFacetSortType.Name ? "index" : "count";
                var sortName = sortAscending.Value ? "asc" : "desc";

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            this._value = new JProperty(aliasName, new JObject(new JProperty("range", new JObject(array.ToArray()))));
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

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
            var facetObject = (JObject)jObject[this.ParameterName] ?? new JObject();

            facetObject.Add(this._value);

            jObject[this.ParameterName] = facetObject;
        }
    }
}
