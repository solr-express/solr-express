using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    [AllowMultipleInstances]
    [FieldMustBeIndexedTrue]
    public sealed class SortParameter<TDocument> : ISortParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

        public SortParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this.ExpressionBuilder = expressionBuilder;
            this.Ascendent = true;
        }

        public bool Ascendent { get; set; }
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>> FieldExpression { get; set; }

        public void AddResultInContainer(List<string> container)
        {
            var value = container.FirstOrDefault(q => q.StartsWith("sort="));

            if (!string.IsNullOrWhiteSpace(value))
            {
                container.Remove(value);

                value += $", {this._result}";
            }
            else
            {
                value = $"sort={this._result}";
            }

            container.Add(value);
        }

        public void Execute()
        {
            var fieldName = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
            this._result = $"{fieldName} {(this.Ascendent ? "asc" : "desc")}";
        }
    }
}
