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
    [FieldMustBeStoredTrue]
    public class FieldsParameter<TDocument> : IFieldsParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        private string _result;

        public FieldsParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        ExpressionBuilder<TDocument> ISearchItemFieldExpressions<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>>[] ISearchItemFieldExpressions<TDocument>.FieldExpressions { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = ((IFieldsParameter<TDocument>)this);
            var fieldNames = parameter
                .FieldExpressions
                .Select(fieldExpression => ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder.GetData(fieldExpression).FieldName)
                .ToArray();

            this._result = $"fl={string.Join(",", fieldNames)}";
        }
    }
}
