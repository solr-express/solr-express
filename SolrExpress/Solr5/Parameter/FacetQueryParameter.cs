using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SolrExpress.Enumerator;
using SolrExpress.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FacetQueryParameter : IQueryParameter
    {
        private readonly JProperty _value;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="sortAscending">Sort ascending the result of the facet</param>
        public FacetQueryParameter(string aliasName, string query, SolrFacetSortType? sortType = null, bool? sortAscending = true)
        {
            var array = new List<JProperty>
            {
                new JProperty("q", query)
            };

            if (sortType.HasValue && sortAscending.HasValue)
            {
                var typeName = sortType.Value == SolrFacetSortType.Name ? "index" : "count";
                var sortName = sortAscending.Value ? "asc" : "desc";

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            this._value = new JProperty(aliasName, new JObject(new JProperty("query", new JObject(array.ToArray()))));
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
