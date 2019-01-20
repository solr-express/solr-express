using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class DefaultFieldParameter<TDocument> : BaseDefaultFieldParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

        public DefaultFieldParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this.ExpressionBuilder = expressionBuilder;
        }

        public void AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        public void Execute()
        {
            var fieldName = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
            this._result = $"df={fieldName}";
        }
    }
}
