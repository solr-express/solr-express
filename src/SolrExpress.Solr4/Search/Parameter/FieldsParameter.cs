using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr4.Search.Parameter
{
    [FieldMustBeStoredTrue]
    public sealed class FieldsParameter<TDocument> : BaseFieldsParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

        public FieldsParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this.ExpressionBuilder = expressionBuilder;
        }

        public void AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        public void Execute()
        {
            var fieldNames = this
                .FieldExpressions
                .Select(fieldExpression => this.ExpressionBuilder.GetFieldName(fieldExpression))
                .ToArray();

            this._result = $"fl={string.Join(",", fieldNames)}";
        }
    }
}
