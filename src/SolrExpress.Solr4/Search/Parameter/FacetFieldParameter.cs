using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using SolrExpress.Solr4.Extension.Internal;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class FacetFieldParameter<TDocument> : BaseFacetFieldParameter<TDocument>, ISearchParameter<List<string>>
        where TDocument : IDocument
    {
              /// Execute the creation of the parameter "facet.field"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var aliasName = ExpressionUtility.GetPropertyNameFromExpression(this.Expression);
            var fieldName = ExpressionUtility.GetFieldNameFromExpression(this.Expression);
            var facetField = this.Excludes.GetSolrFacetWithExcludes(aliasName, fieldName);

            container.Add($"facet.field={facetField}");

            if (this.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(this.SortType.Value == FacetSortType.CountDesc || this.SortType.Value == FacetSortType.IndexDesc);

                ExpressionUtility.GetSolrFacetSort(this.SortType.Value, out typeName, out dummy);

                container.Add($"f.{fieldName}.facet.sort={typeName}");
            }

            container.Add($"f.{fieldName}.facet.mincount=1");

            if (this.Limit.HasValue)
            {
                container.Add($"f.{fieldName}.facet.limit={this.Limit.Value}");
            }
        }
    }
}
