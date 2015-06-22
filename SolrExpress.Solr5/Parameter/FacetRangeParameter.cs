using Newtonsoft.Json.Linq;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FacetRangeParameter<T> : IParameter<JObject>, IValidation
      where T : IDocument
    {
        private readonly Expression<Func<T, object>> _expression;
        private readonly string _aliasName;
        private readonly string _gap;
        private readonly string _start;
        private readonly string _end;
        private readonly SolrFacetSortType? _sortType;

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
            this._expression = expression;
            this._aliasName = aliasName;
            this._gap = gap;
            this._start = start;
            this._end = end;
            this._sortType = sortType;
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

            var fieldName = UtilHelper.GetFieldNameFromExpression(this._expression);

            var array = new List<JProperty>
            {
                new JProperty("field", fieldName)
            };

            if (!string.IsNullOrWhiteSpace(this._gap))
            {
                array.Add(new JProperty("gap", this._gap));
            }
            if (!string.IsNullOrWhiteSpace(this._start))
            {
                array.Add(new JProperty("start", this._start));
            }
            if (!string.IsNullOrWhiteSpace(this._end))
            {
                array.Add(new JProperty("end", this._end));
            }

            array.Add(new JProperty("other", new JArray("before", "after")));

            if (this._sortType.HasValue)
            {
                string typeName;
                string sortName;

                UtilHelper.GetSolrFacetSort(this._sortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            var value = new JProperty(this._aliasName, new JObject(new JProperty("range", new JObject(array.ToArray()))));

            facetObject.Add(value);

            jObject["facet"] = facetObject;
        }

        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            isValid = true;
            errorMessage = string.Empty;
            //TODO: Unit test
            var solrFieldAttribute = UtilHelper.GetSolrFieldAttributeFromPropertyInfo(this._expression);

            if (solrFieldAttribute != null && !solrFieldAttribute.Indexed)
            {
                isValid = false;
                errorMessage = "A field must be \"indexed=true\" to be used in a facet";
            }
        }
    }
}
