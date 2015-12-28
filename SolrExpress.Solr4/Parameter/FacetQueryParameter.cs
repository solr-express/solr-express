using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetQueryParameter : IFacetQueryParameter, IParameter<List<string>>, IValidation
    {
        private readonly string _aliasName;
        private readonly IQueryParameterValue _query;
        private readonly SolrFacetSortType? _sortType;
        private readonly string[] _excludes;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public FacetQueryParameter(string aliasName, IQueryParameterValue query, SolrFacetSortType? sortType = null, params string[] excludes)
        {
            ThrowHelper<ArgumentNullException>.If(string.IsNullOrWhiteSpace(aliasName));
            ThrowHelper<ArgumentNullException>.If(query == null);

            this._aliasName = aliasName;
            this._query = query;
            this._sortType = sortType;
            this._excludes = excludes;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var query = this._query.Execute();

            container.Add($"facet.query={UtilHelper.GetSolrFacetWithExcludesSolr4(this._aliasName, query, this._excludes)}");

            if (this._sortType.HasValue)
            {
                string typeName;
                string dummy;

                ThrowHelper<UnsupportedSortTypeException>.If(this._sortType.Value == SolrFacetSortType.CountDesc || this._sortType.Value == SolrFacetSortType.IndexDesc);

                UtilHelper.GetSolrFacetSort(this._sortType.Value, out typeName, out dummy);

                container.Add($"f.{this._aliasName}.facet.sort={typeName}");
            }

            container.Add($"f.{this._aliasName}.facet.mincount=1");
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
