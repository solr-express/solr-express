using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    [AllowMultipleInstances]
    [FacetRangeType]
    [FieldMustBeIndexedTrue]
    public class FacetRangeParameter<TDocument> : IFacetRangeParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private readonly List<string> _result = new List<string>();

        public FacetRangeParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        string IFacetRangeParameter<TDocument>.AliasName { get; set; }

        bool IFacetRangeParameter<TDocument>.CountAfter { get; set; }

        bool IFacetRangeParameter<TDocument>.CountBefore { get; set; }

        string IFacetRangeParameter<TDocument>.End { get; set; }

        string[] IFacetRangeParameter<TDocument>.Excludes { get; set; }

        ExpressionBuilder<TDocument> ISearchItemFieldExpression<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>> ISearchItemFieldExpression<TDocument>.FieldExpression { get; set; }

        string IFacetRangeParameter<TDocument>.Gap { get; set; }

        int? IFacetRangeParameter<TDocument>.Limit { get; set; }

        int? IFacetRangeParameter<TDocument>.Minimum { get; set; }

        FacetSortType? IFacetRangeParameter<TDocument>.SortType { get; set; }

        string IFacetRangeParameter<TDocument>.Start { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            container.AddRange(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = (IFacetRangeParameter<TDocument>)this;
            var fieldName = ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder.GetFieldName(parameter.FieldExpression);
            var facetName = ParameterUtil.GetFacetName(parameter.Excludes, parameter.AliasName, fieldName);

            this._result.Add($"facet.range={facetName}");

            if (!string.IsNullOrWhiteSpace(parameter.Gap))
            {
                this._result.Add($"f.{fieldName}.facet.range.gap={Uri.EscapeDataString(parameter.Gap)}");
            }
            if (!string.IsNullOrWhiteSpace(parameter.Start))
            {
                this._result.Add($"f.{fieldName}.facet.range.start={Uri.EscapeDataString(parameter.Start)}");
            }
            if (!string.IsNullOrWhiteSpace(parameter.End))
            {
                this._result.Add($"f.{fieldName}.facet.range.end={Uri.EscapeDataString(parameter.End)}");
            }

            if (parameter.CountBefore)
            {
                this._result.Add($"f.{fieldName}.facet.range.other=before");
            }

            if (parameter.CountAfter)
            {
                this._result.Add($"f.{fieldName}.facet.range.other=after");
            }

            if (parameter.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(parameter.SortType.Value == FacetSortType.CountDesc || parameter.SortType.Value == FacetSortType.IndexDesc);

                ParameterUtil.GetFacetSort(parameter.SortType.Value, out typeName, out dummy);

                this._result.Add($"f.{fieldName}.facet.sort={typeName}");
            }

            if (parameter.Minimum.HasValue)
            {
                this._result.Add($"f.{fieldName}.facet.mincount={parameter.Minimum.Value}");
            }

            if (parameter.Limit.HasValue)
            {
                this._result.Add($"f.{fieldName}.facet.limit={parameter.Limit.Value}");
            }
        }
    }
}