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
    [FieldMustBeIndexedTrue]
    public class FacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private readonly List<string> _result = new List<string>();

        public FacetFieldParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        string[] IFacetFieldParameter<TDocument>.Excludes { get; set; }

        ExpressionBuilder<TDocument> ISearchItemFieldExpression<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>> ISearchItemFieldExpression<TDocument>.FieldExpression { get; set; }

        int? IFacetFieldParameter<TDocument>.Limit { get; set; }

        int? IFacetFieldParameter<TDocument>.Minimum { get; set; }

        FacetSortType? IFacetFieldParameter<TDocument>.SortType { get; set; }

        IEnumerable<IFacetParameter> IFacetParameter.Facets { get; set; }

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
            var parameter = (IFacetFieldParameter<TDocument>)this;

            Checker.IsNull(parameter.FieldExpression);

            var aliasName = ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder.GetAliasName(parameter.FieldExpression);
            var fieldName = ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder.GetFieldName(parameter.FieldExpression);
            var facetField = ParameterUtil.GetFacetName(parameter.Excludes, aliasName, fieldName);

            this._result.Add($"facet.field={facetField}");

            if (parameter.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(parameter.SortType == FacetSortType.CountDesc || parameter.SortType == FacetSortType.IndexDesc);

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
