using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Core.Query;
using System.Collections.Generic;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FacetQueryParameter<TDocument> : IFacetQueryParameter<TDocument>, IParameter<JObject>, IValidation
         where TDocument : IDocument
    {
        private string _aliasName;
        private IQueryParameterValue _query;
        private SolrFacetSortType? _sortType;
        private string[] _excludes;

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var facetObject = (JObject)jObject["facet"] ?? new JObject();

            //TODO
            //var array = new List<JProperty>
            //{
            //    new JProperty("q", UtilHelper.GetSolrFacetWithExcludesSolr5(this._query.Execute(), this._excludes))
            //};

            //if (this._sortType.HasValue)
            //{
            //    string typeName;
            //    string sortName;

            //    UtilHelper.GetSolrFacetSort(this._sortType.Value, out typeName, out sortName);

            //    array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            //}

            //var jProperty = new JProperty(this._aliasName, new JObject(new JProperty("query", new JObject(array.ToArray()))));

            //facetObject.Add(jProperty);

            //jObject["facet"] = facetObject;
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

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public IFacetQueryParameter<TDocument> Configure(string aliasName, IQueryParameterValue query, SolrFacetSortType? sortType = null, params string[] excludes)
        {
            Checker.IsNullOrWhiteSpace(aliasName);
            Checker.IsNull(query);

            this._aliasName = aliasName;
            this._query = query;
            this._sortType = sortType;
            this._excludes = excludes;

            return this;
        }
    }
}
