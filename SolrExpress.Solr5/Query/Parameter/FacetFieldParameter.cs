using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr5.Extension.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Query.Parameter
{
    public sealed class FacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, IParameter<JObject>, IValidation
        where TDocument : IDocument
    {
        private Expression<Func<TDocument, object>> _expression;
        private FacetSortType? _sortType;
        private int? _limit;
        private string[] _excludes;

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Execute the creation of the parameter "facet.field"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var facetObject = (JObject)jObject["facet"] ?? new JObject();

            var fieldName = this._expression.GetFieldNameFromExpression();
            var aliasName = this._expression.GetPropertyNameFromExpression();

            var array = new List<JProperty>
            {
                new JProperty("field", this._excludes.GetSolrFacetWithExcludes(fieldName))
            };

            if (_sortType.HasValue)
            {
                string typeName;
                string sortName;

                _sortType.Value.GetSolrFacetSort(out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            if (this._limit.HasValue)
            {
                array.Add(new JProperty("limit", this._limit));
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

            var solrFieldAttribute = this._expression.GetSolrFieldAttributeFromPropertyInfo();

            if (solrFieldAttribute == null || solrFieldAttribute.Indexed)
            {
                return;
            }

            isValid = false;
            errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAFacetException;
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public IFacetFieldParameter<TDocument> Configure(Expression<Func<TDocument, object>> expression, FacetSortType? sortType = null, int? limit = null, params string[] excludes)
        {
            Checker.IsNull(expression);

            this._expression = expression;
            this._sortType = sortType;
            this._limit = limit;
            this._excludes = excludes;

            return this;
        }
    }
}
