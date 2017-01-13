using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using SolrExpress.Solr4.Extension.Internal;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class FacetRangeParameter<TDocument> : BaseFacetRangeParameter<TDocument>, ISearchParameterExecute<List<string>>
      where TDocument : IDocument
    {
        public FacetRangeParameter(IExpressionBuilder<TDocument> expressionBuilder) : base(expressionBuilder)
        {
        }

        /// <summary>
        /// Execute the creation of the parameter "facet.range"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var fieldName = this._expressionBuilder.GetFieldNameFromExpression(this.Expression);
            var facetName = this.Excludes.GetSolrFacetWithExcludes(this.AliasName, fieldName);

            container.Add($"facet.range={facetName}");

            if (!string.IsNullOrWhiteSpace(this.Gap))
            {
                container.Add($"f.{fieldName}.facet.range.gap={Uri.EscapeDataString(this.Gap)}");
            }
            if (!string.IsNullOrWhiteSpace(this.Start))
            {
                container.Add($"f.{fieldName}.facet.range.start={Uri.EscapeDataString(this.Start)}");
            }
            if (!string.IsNullOrWhiteSpace(this.End))
            {
                container.Add($"f.{fieldName}.facet.range.end={Uri.EscapeDataString(this.End)}");
            }

            if (this.CountBefore)
            {
                container.Add($"f.{fieldName}.facet.range.other=before");
            }

            if (this.CountAfter)
            {
                container.Add($"f.{fieldName}.facet.range.other=after");
            }

            if (this.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(this.SortType.Value == FacetSortType.CountDesc || this.SortType.Value == FacetSortType.IndexDesc);

                ExpressionUtility.GetSolrFacetSort(this.SortType.Value, out typeName, out dummy);

                container.Add($"f.{fieldName}.facet.range.sort={typeName}");
            }

            container.Add($"f.{fieldName}.facet.mincount=1");
        }
    }
}
