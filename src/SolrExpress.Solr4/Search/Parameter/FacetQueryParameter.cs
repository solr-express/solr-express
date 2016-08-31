using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using SolrExpress.Solr4.Extension.Internal;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class FacetQueryParameter<TDocument> : BaseFacetQueryParameter<TDocument>, ISearchParameter<List<string>>
        where TDocument : IDocument
    {
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

            var query = this.Query.Execute();

            container.Add($"facet.query={this.Excludes.GetSolrFacetWithExcludes(this.AliasName, query)}");

            if (this.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(this.SortType.Value == FacetSortType.CountDesc || this.SortType.Value == FacetSortType.IndexDesc);

                ExpressionUtility.GetSolrFacetSort(this.SortType.Value, out typeName, out dummy);

                container.Add($"f.{this.AliasName}.facet.sort={typeName}");
            }

            container.Add($"f.{this.AliasName}.facet.mincount=1");
        }
    }
}
