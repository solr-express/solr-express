using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    [AllowMultipleInstances]
    [FieldMustBeIndexedTrue]
    public sealed class FacetFieldParameter<TDocument> : BaseFacetFieldParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private readonly List<string> _result = new List<string>();

        public FacetFieldParameter(ExpressionBuilder<TDocument> expressionBuilder, ISolrExpressServiceProvider<TDocument> serviceProvider)
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
            Checker.IsNull(this.FieldExpression);
            Checker.IsTrue<UnsupportedFeatureException>(this.Filter != null);

            var aliasName = this.ExpressionBuilder.GetAliasName(this.FieldExpression);
            var fieldName = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
            var facetField = ParameterUtil.GetFacetName(this.Excludes, aliasName, fieldName);

            this._result.Add($"facet.field={facetField}");

            if (this.SortType.HasValue)
            {
                Checker.IsTrue<UnsupportedFeatureException>(this.SortType == FacetSortType.CountDesc || this.SortType == FacetSortType.IndexDesc);

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

            if (this.MethodType.HasValue)
            {
                var methodName = string.Empty;
                switch (this.MethodType.Value)
                {
                    case FacetMethodType.UninvertedField:
                        methodName = "fc";
                        break;
                    case FacetMethodType.DocValues:
                        methodName = "enum";
                        break;
                    case FacetMethodType.Stream:
                        methodName = "fcs";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(this.MethodType));
                }

                this._result.Add($"f.{fieldName}.facet.method={methodName}");
            }

            if (!string.IsNullOrWhiteSpace(this.Prefix))
            {
                this._result.Add($"f.{fieldName}.facet.prefix={this.Prefix}");
            }
        }
    }
}
