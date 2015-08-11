using Newtonsoft.Json.Linq;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FacetFieldParameter<T> : IParameter<JObject>, IValidation
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
            Contract.Requires<ArgumentNullException>(expression != null);

            this._expression = expression;
            this._sortType = sortType;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

        /// <summary>
        /// Execute the creation of the parameter "facet.field"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var facetObject = (JObject)jObject["facet"] ?? new JObject();

            var fieldName = UtilHelper.GetFieldNameFromExpression(this._expression);
            var aliasName = UtilHelper.GetPropertyNameFromExpression(this._expression);

            var array = new List<JProperty>
            {
                new JProperty("field", fieldName)
            };

            if (_sortType.HasValue)
            {
                string typeName;
                string sortName;

                UtilHelper.GetSolrFacetSort(_sortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            var value = new JProperty(aliasName, new JObject(new JProperty("terms", new JObject(array.ToArray()))));

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
            
            var solrFieldAttribute = UtilHelper.GetSolrFieldAttributeFromPropertyInfo(this._expression);

            if (solrFieldAttribute != null && !solrFieldAttribute.Indexed)
            {
                isValid = false;
                errorMessage = "A field must be \"indexed=true\" to be used in a facet";
            }
        }
    }
}
