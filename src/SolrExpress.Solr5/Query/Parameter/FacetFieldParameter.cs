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
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; private set; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        public FacetSortType? SortType { get; private set; }

        /// <summary>
        /// Limit of itens in facet's result
        /// </summary>
        public int? Limit { get; private set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        public string[] Excludes { get; private set; }

        /// <summary>
        /// Execute the creation of the parameter "facet.field"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var facetObject = (JObject)jObject["facet"] ?? new JObject();

            var fieldName = this.Expression.GetFieldNameFromExpression();
            var aliasName = this.Expression.GetPropertyNameFromExpression();

            var array = new List<JProperty>
            {
                new JProperty("field", this.Excludes.GetSolrFacetWithExcludes(fieldName))
            };

            array.Add(new JProperty("mincount", 1));

            if (this.SortType.HasValue)
            {
                string typeName;
                string sortName;

                this.SortType.Value.GetSolrFacetSort(out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            if (this.Limit.HasValue)
            {
                array.Add(new JProperty("limit", this.Limit));
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

            var solrFieldAttribute = this.Expression.GetSolrFieldAttributeFromPropertyInfo();

            if (solrFieldAttribute?.Indexed ?? true)
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

            this.Expression = expression;
            this.SortType = sortType;
            this.Limit = limit;
            this.Excludes = excludes;

            return this;
        }
    }
}
