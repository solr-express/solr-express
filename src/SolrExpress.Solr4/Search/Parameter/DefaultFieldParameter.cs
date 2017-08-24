using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class DefaultFieldParameter<TDocument> : IDefaultFieldParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

        public DefaultFieldParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        ExpressionBuilder<TDocument> ISearchItemFieldExpression<TDocument>.ExpressionBuilder { get;set; }
        Expression<Func<TDocument, object>> ISearchItemFieldExpression<TDocument>.FieldExpression { get;set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = (IDefaultFieldParameter<TDocument>)this;
            var fieldName = parameter.ExpressionBuilder.GetFieldName(parameter.FieldExpression);
            this._result = $"df={fieldName}";
        }
    }
}
