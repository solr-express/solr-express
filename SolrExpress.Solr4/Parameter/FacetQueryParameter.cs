using SolrExpress.Core;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Core.Query;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetQueryParameter<TDocument> : IFacetQueryParameter<TDocument>, IParameter<List<string>>, IValidation
        where TDocument : IDocument
    {
        /// <summary>
        /// Create a facet parameter
        /// </summary>
        public FacetQueryParameter()
        {
        }

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public FacetQueryParameter(string aliasName, IQueryParameterValue query, SolrFacetSortType? sortType = null, params string[] excludes)
            : this()
        {
            this.AliasName = aliasName;
            this.Query = query;
            this.SortType = sortType;
            this.Excludes = excludes?.ToList();
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
            Checker.IsNullOrWhiteSpace(this.AliasName);
            Checker.IsNull(this.Query);

            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var query = this.Query.Execute();

            //TODO
            //container.Add($"facet.query={UtilHelper.GetSolrFacetWithExcludesSolr4(this._aliasName, query, this._excludes)}");

            if (this.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(this.SortType.Value == SolrFacetSortType.CountDesc || this.SortType.Value == SolrFacetSortType.IndexDesc);

                //TODO
                //UtilHelper.GetSolrFacetSort(this._sortType.Value, out typeName, out dummy);

                //container.Add($"f.{this._aliasName}.facet.sort={typeName}");
            }

            container.Add($"f.{this.AliasName}.facet.mincount=1");
        }

        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            Checker.IsNullOrWhiteSpace(this.AliasName);
            Checker.IsNull(this.Query);

            isValid = true;
            errorMessage = string.Empty;

            var queryValidation = this.Query as IValidation;

            if (queryValidation != null)
            {
                queryValidation.Validate(out isValid, out errorMessage);
            }
        }

        /// <summary>
        /// Name of the alias added in the query
        /// </summary>
        public string AliasName { get; set; }

        /// <summary>
        /// Query used to make the facet
        /// </summary>
        public IQueryParameterValue Query { get; set; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        public SolrFacetSortType? SortType { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        public List<string> Excludes { get; set; }
    }
}
