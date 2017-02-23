using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class SortParameter<TDocument> : ISortParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        private readonly ExpressionBuilder<TDocument> _expressionBuilder;
        private string _result;

        public SortParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        bool ISortParameter<TDocument>.Ascendent { get; set; }

        Expression<Func<TDocument, object>> ISortParameter<TDocument>.FieldExpression { get; set; }

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

            var fieldName = this._expressionBuilder.GetFieldNameFromExpression(parameter.FieldExpression);
            this._result = $"{fieldName} {(parameter.Ascendent ? "asc" : "desc")}";
        }
    }
}
