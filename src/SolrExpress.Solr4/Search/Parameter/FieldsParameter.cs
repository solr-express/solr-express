using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class FieldsParameter<TDocument> : IFieldsParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        private readonly ExpressionBuilder<TDocument> _expressionBuilder;
        private string _result;

        public FieldsParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        bool ISearchParameter.AllowMultipleInstances { get; set; }

        Expression<Func<TDocument, object>>[] IFieldsParameter<TDocument>.FieldExpressions { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = ((IFieldsParameter<TDocument>)this);
            var fieldNames = parameter
                .FieldExpressions
                .Select(fieldExpression => this._expressionBuilder.GetFieldNameFromExpression(fieldExpression))
                .ToArray();

            this._result = $"fl={string.Join(",", fieldNames)}";
        }
    }
}
