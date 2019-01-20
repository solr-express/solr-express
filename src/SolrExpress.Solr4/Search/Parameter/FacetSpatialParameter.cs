using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    [AllowMultipleInstances]
    [FieldMustBeIndexedTrue]
    public sealed class FacetSpatialParameter<TDocument> : BaseFacetSpatialParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private readonly List<string> _result = new List<string>();

        public FacetSpatialParameter(ExpressionBuilder<TDocument> expressionBuilder, ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            this.ExpressionBuilder = expressionBuilder;
            this.ServiceProvider = serviceProvider;
        }

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
            var fieldName = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
            var formule = ParameterUtil.GetSpatialFormule(fieldName, this.FunctionType, this.CenterPoint, this.Distance);
            var facetName = ParameterUtil.GetFacetName(this.Excludes, this.AliasName, formule);

            this._result.Add($"facet.query={facetName}");

            if (this.SortType.HasValue)
            {
                Checker.IsTrue<UnsupportedFeatureException>(this.SortType.Value == FacetSortType.CountDesc || this.SortType.Value == FacetSortType.IndexDesc);

                ParameterUtil.GetFacetSort(this.SortType.Value, out string typeName, out string dummy);

                this._result.Add($"f.{this.AliasName}.facet.sort={typeName}");
            }

            if (this.Minimum.HasValue)
            {
                this._result.Add($"f.{this.AliasName}.facet.mincount={this.Minimum.Value}");
            }

            if (this.Limit.HasValue)
            {
                this._result.Add($"f.{this.AliasName}.facet.limit={this.Limit.Value}");
            }
        }
    }
}
