using Newtonsoft.Json.Linq;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetFieldParameter<T> : IParameter
        where T : IDocument
    {
        private readonly Expression<Func<T, object>> _expression;
        private readonly SolrFacetSortType? _sortType;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public FacetFieldParameter(Expression<Func<T, object>> expression, SolrFacetSortType? sortType = null)
        {
            this._expression = expression;
            this._sortType = sortType;



            var array = new List<JProperty>
            {
                new JProperty("field", fieldName)
            };

            if (sortType.HasValue)
            {
                string typeName;
                string sortName;

                UtilHelper.GetSolrFacetSort(sortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            this._value = new JProperty(fieldName, new JObject(new JProperty("terms", new JObject(array.ToArray()))));
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

            var fieldName = UtilHelper.GetPropertyNameFromExpression(this._expression);
            jObject["facet.field"]=new JProperty("facet.field",true);


            var facetObject = (JProperty)jObject["facet"] ?? new JProperty("facet",);

            facetObject.Add(this._value);

            jObject["facet"] = facetObject;
        }
    }
}
