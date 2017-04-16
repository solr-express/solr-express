using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    [AllowMultipleInstances]
    [FieldMustBeIndexedTrue]
    public class SortParameter<TDocument> : ISortParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        private string _result;

        public SortParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchParameterFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        bool ISortParameter<TDocument>.Ascendent { get; set; }

        ExpressionBuilder<TDocument> ISearchParameterFieldExpression<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>> ISearchParameterFieldExpression<TDocument>.FieldExpression { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            var value = container.FirstOrDefault(q => q.StartsWith("sort="));

            if (!string.IsNullOrWhiteSpace(value))
            {
                container.Remove(value);

                value += $", {this._result}";
            }
            else
            {
                value = this._result;
            }

            container.Add(value);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = (ISortParameter<TDocument>)this;

            var fieldName = ((ISearchParameterFieldExpression<TDocument>)this).ExpressionBuilder.GetFieldName(parameter.FieldExpression);
            this._result = $"{fieldName} {(parameter.Ascendent ? "asc" : "desc")}";
        }
    }
}
