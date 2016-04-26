using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr4.Extension.Internal;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetQueryParameter<TDocument> : IFacetQueryParameter<TDocument>, IParameter<List<string>>, IValidation
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
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var query = this._query.Execute();

            container.Add($"facet.query={this._excludes.GetSolrFacetWithExcludes(this._aliasName, query)}");

            if (this._sortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(this._sortType.Value == SolrFacetSortType.CountDesc || this._sortType.Value == SolrFacetSortType.IndexDesc);

                this._sortType.Value.GetSolrFacetSort(out typeName, out dummy);

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
