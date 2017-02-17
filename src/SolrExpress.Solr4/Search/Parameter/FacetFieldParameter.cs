using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class FacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        private readonly ExpressionBuilder<TDocument> _expressionBuilder;
        private readonly List<string> _result = new List<string>();

        public FacetFieldParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        bool ISearchParameter.AllowMultipleInstances { get; set; }

        string[] IFacetFieldParameter<TDocument>.Excludes { get; set; }

        Expression<Func<TDocument, object>> IFacetFieldParameter<TDocument>.FieldExpression { get; set; }

        int? IFacetFieldParameter<TDocument>.Limit { get; set; }

        int? IFacetFieldParameter<TDocument>.Minimum { get; set; }

        FacetSortType? IFacetFieldParameter<TDocument>.SortType { get; set; }

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

            var aliasName = this._expressionBuilder.GetPropertyNameFromExpression(parameter.FieldExpression);
            var fieldName = this._expressionBuilder.GetFieldNameFromExpression(parameter.FieldExpression);
            var facetField = ParameterUtil.GetFacetName(parameter.Excludes, aliasName, fieldName);

            this._result.Add($"facet.field={facetField}");

            if (parameter.SortType.HasValue)
            {
                string typeName;
                string dummy;

                //TODO: Create exception
                //Checker.IsTrue<UnsupportedSortTypeException>(parameter.SortType == FacetSortType.CountDesc || parameter.SortType == FacetSortType.IndexDesc);

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
