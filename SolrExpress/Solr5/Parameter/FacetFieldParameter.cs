using Newtonsoft.Json.Linq;
using SolrExpress.Enumerator;
using SolrExpress.Helper;
using SolrExpress.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FacetFieldParameter<T> : IQueryParameter
        where T : IDocument
    {
        private readonly JProperty _value;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="sortAscending">Sort ascending the result of the facet</param>
        public FacetFieldParameter(Expression<Func<T, object>> expression, SolrFacetSortType? sortType = null, bool? sortAscending = true)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            var array = new List<JProperty>
            {
                new JProperty("field", fieldName)
            };

            if (sortType.HasValue && sortAscending.HasValue)
            {
                var typeName = sortType.Value == SolrFacetSortType.Name ? "index" : "count";
                var sortName = sortAscending.Value ? "asc" : "desc";

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            this._value = new JProperty(fieldName, new JObject(new JProperty("terms", new JObject(array.ToArray()))));
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
