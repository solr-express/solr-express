using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using SolrExpress.Solr4.Extension.Internal;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class FacetSpatialParameter<TDocument> : BaseFacetSpatialParameter<TDocument>, ISearchParameter<List<string>>
        where TDocument : IDocument
    {
        public FacetSpatialParameter(IExpressionBuilder<TDocument> expressionBuilder) : base(expressionBuilder)
        {
        }

        /// <summary>
        /// Execute creation of the parameter "facet field" using spatial formule
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            Checker.IsNullOrWhiteSpace(this.AliasName);
            Checker.IsNull(this.Expression);

            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var fieldName = this._expressionBuilder.GetFieldNameFromExpression(this.Expression);
            var formule = ExpressionUtility.GetSolrSpatialFormule(this.FunctionType, fieldName, this.CenterPoint, this.Distance);
            var facetName = this.Excludes.GetSolrFacetWithExcludes(this.AliasName, formule);

            container.Add($"facet.query={facetName}");

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
