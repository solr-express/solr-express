using Newtonsoft.Json.Linq;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System.Collections.Generic;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FacetQueryParameter : IParameter<JObject>
    {
        private readonly JProperty _value;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public FacetQueryParameter(string aliasName, string query, SolrFacetSortType? sortType = null)
        {
            var array = new List<JProperty>
            {
                new JProperty("q", query)
            };

            if (sortType.HasValue)
            {
                string typeName;
                string sortName;

                UtilHelper.GetSolrFacetSort(sortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            this._value = new JProperty(aliasName, new JObject(new JProperty("query", new JObject(array.ToArray()))));
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
            var facetObject = (JObject)jObject["facet"] ?? new JObject();

            facetObject.Add(this._value);

            jObject["facet"] = facetObject;
        }
    }
}
