using SolrExpress.Builder;
using SolrExpress.Core.Search.Parameter;
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
    [FieldMustBeIndexedTrue]
    public class FacetSpatialParameter<TDocument> : IFacetSpatialParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        private readonly List<string> _result = new List<string>();

        public FacetSpatialParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        string IFacetSpatialParameter<TDocument>.AliasName { get; set; }
        
        GeoCoordinate IFacetSpatialParameter<TDocument>.CenterPoint { get; set; }

        decimal IFacetSpatialParameter<TDocument>.Distance { get; set; }

        string[] IFacetSpatialParameter<TDocument>.Excludes { get; set; }

        ExpressionBuilder<TDocument> ISearchItemFieldExpression<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>> ISearchItemFieldExpression<TDocument>.FieldExpression { get; set; }

        SpatialFunctionType IFacetSpatialParameter<TDocument>.FunctionType { get; set; }

        int? IFacetSpatialParameter<TDocument>.Limit { get; set; }

        int? IFacetSpatialParameter<TDocument>.Minimum { get; set; }

        FacetSortType? IFacetSpatialParameter<TDocument>.SortType { get; set; }

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
            var parameter = (IFacetSpatialParameter<TDocument>)this;
            var data = ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder.GetData(parameter.FieldExpression);
            var formule = ParameterUtil.GetSpatialFormule(data.FieldName, parameter.FunctionType, parameter.CenterPoint, parameter.Distance);
            var facetName = ParameterUtil.GetFacetName(parameter.Excludes, parameter.AliasName, formule);

            this._result.Add($"facet.query={facetName}");

            if (parameter.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(parameter.SortType.Value == FacetSortType.CountDesc || parameter.SortType.Value == FacetSortType.IndexDesc);

                ParameterUtil.GetFacetSort(parameter.SortType.Value, out typeName, out dummy);

                this._result.Add($"f.{parameter.AliasName}.facet.sort={typeName}");
            }

            if (parameter.Minimum.HasValue)
            {
                this._result.Add($"f.{parameter.AliasName}.facet.mincount={parameter.Minimum.Value}");
            }

            if (parameter.Limit.HasValue)
            {
                this._result.Add($"f.{parameter.AliasName}.facet.limit={parameter.Limit.Value}");
            }
        }
    }
}
