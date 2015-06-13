using Newtonsoft.Json.Linq;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FacetRangeParameter<T> : IParameter<JObject>
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
        public FacetRangeParameter(Expression<Func<T, object>> expression, string aliasName, string gap = null, string start = null, string end = null, SolrFacetSortType? sortType = null)
        {
            var fieldName = UtilHelper.GetFieldNameFromExpression(expression);

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

            if (sortType.HasValue)
            {
                string typeName;
                string sortName;

                UtilHelper.GetSolrFacetSort(sortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            this._value = new JProperty(aliasName, new JObject(new JProperty("range", new JObject(array.ToArray()))));
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
