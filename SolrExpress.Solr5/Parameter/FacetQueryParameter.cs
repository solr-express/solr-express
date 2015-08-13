using Newtonsoft.Json.Linq;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FacetQueryParameter : IParameter<JObject>, IValidation
    {
        private readonly string _aliasName;
        private readonly IQueryParameterValue _query;
        private readonly SolrFacetSortType? _sortType;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public FacetQueryParameter(string aliasName, IQueryParameterValue query, SolrFacetSortType? sortType = null)
        {
            ThrowHelper<ArgumentNullException>.If(string.IsNullOrWhiteSpace(aliasName));
            ThrowHelper<ArgumentNullException>.If(query == null);

            this._aliasName = aliasName;
            this._query = query;
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

            var array = new List<JProperty>
            {
                new JProperty("q", this._query.Execute())
            };

            if (this._sortType.HasValue)
            {
                string typeName;
                string sortName;

                UtilHelper.GetSolrFacetSort(this._sortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }
            
            var jProperty = new JProperty(this._aliasName, new JObject(new JProperty("query", new JObject(array.ToArray()))));

            facetObject.Add(jProperty);

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
            
            var queryValidation = this._query as IValidation;

            if (queryValidation != null)
            {
                queryValidation.Validate(out isValid, out errorMessage);
            }
        }
    }
}
