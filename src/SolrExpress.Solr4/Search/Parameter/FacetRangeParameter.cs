using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    [AllowMultipleInstances]
    [FacetRangeType]
    [FieldMustBeIndexedTrue]
    public sealed class FacetRangeParameter<TDocument> : IFacetRangeParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private readonly List<string> _result = new List<string>();

        public FacetRangeParameter(ExpressionBuilder<TDocument> expressionBuilder, ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            this.ExpressionBuilder = expressionBuilder;
            this.ServiceProvider = serviceProvider;
        }

        public string AliasName { get; set; }
        public bool CountAfter { get; set; }
        public bool CountBefore { get; set; }
        public string End { get; set; }
        public string[] Excludes { get; set; }
        public IList<IFacetParameter<TDocument>> Facets { get; set; }
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>> FieldExpression { get; set; }
        public string Gap { get; set; }
        public int? Limit { get; set; }
        public int? Minimum { get; set; }
        public FacetSortType? SortType { get; set; }
        public string Start { get; set; }
        public ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }
        public SearchQuery<TDocument> Filter { get; set; }

        public void AddResultInContainer(List<string> container)
        {
            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            container.AddRange(this._result);
        }

        public void Execute()
        {
            Checker.IsTrue<UnsupportedFeatureException>(this.Filter != null);

            var fieldName = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
            var facetName = ParameterUtil.GetFacetName(this.Excludes, this.AliasName, fieldName);

            this._result.Add($"facet.range={facetName}");

            if (!string.IsNullOrWhiteSpace(this.Gap))
            {
                this._result.Add($"f.{fieldName}.facet.range.gap={Uri.EscapeDataString(this.Gap)}");
            }
            if (!string.IsNullOrWhiteSpace(this.Start))
            {
                this._result.Add($"f.{fieldName}.facet.range.start={Uri.EscapeDataString(this.Start)}");
            }
            if (!string.IsNullOrWhiteSpace(this.End))
            {
                this._result.Add($"f.{fieldName}.facet.range.end={Uri.EscapeDataString(this.End)}");
            }

            if (this.CountBefore)
            {
                this._result.Add($"f.{fieldName}.facet.range.other=before");
            }

            if (this.CountAfter)
            {
                this._result.Add($"f.{fieldName}.facet.range.other=after");
            }

            if (this.SortType.HasValue)
            {
                Checker.IsTrue<UnsupportedFeatureException>(this.SortType.Value == FacetSortType.CountDesc || this.SortType.Value == FacetSortType.IndexDesc);

                ParameterUtil.GetFacetSort(this.SortType.Value, out string typeName, out string dummy);

                this._result.Add($"f.{fieldName}.facet.sort={typeName}");
            }

            if (this.Minimum.HasValue)
            {
                this._result.Add($"f.{fieldName}.facet.mincount={this.Minimum.Value}");
            }

            if (this.Limit.HasValue)
            {
                this._result.Add($"f.{fieldName}.facet.limit={this.Limit.Value}");
            }
        }
    }
}