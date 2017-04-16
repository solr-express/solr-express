﻿using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
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
            ((ISearchParameterFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        ExpressionBuilder<TDocument> ISearchParameterFieldExpressions<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>>[] ISearchParameterFieldExpressions<TDocument>.FieldExpressions { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = ((IFieldsParameter<TDocument>)this);
            var fieldNames = parameter
                .FieldExpressions
                .Select(fieldExpression => ((ISearchParameterFieldExpression<TDocument>)this).ExpressionBuilder.GetFieldName(fieldExpression))
                .ToArray();

            this._result = $"fl={string.Join(",", fieldNames)}";
        }
    }
}
