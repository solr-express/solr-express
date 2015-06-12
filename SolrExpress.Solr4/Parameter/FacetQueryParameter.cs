using System.Collections.Generic;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetQueryParameter : IParameter<List<string>>
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
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var query = this._query.Execute();

            container.Add(string.Format("facet.query={{!ex=dt key={0}}}{1}", this._aliasName, query));
            
            if (this._sortType.HasValue)
            {
                string typeName;
                string dummy;

                // TODO: In SOLR 4, we can't choise between ascending or descending sort. Make a choise here, throws a exception case the SolrFacetSortType equals *Descending or not throws???
                UtilHelper.GetSolrFacetSort(this._sortType.Value, out typeName, out dummy);

                container.Add(string.Format("f.{0}.facet.sort={1}", this._aliasName, typeName));
            }
        }
    }
}
