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

        bool ISearchParameter.AllowMultipleInstances { get; set; }

        bool ISortParameter<TDocument>.Ascendent { get; set; }

        Expression<Func<TDocument, object>>[] ISortParameter<TDocument>.FieldExpressions { get; set; }

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

            this._result = $"sort={string.Join(",", fieldNames)}";
        }
    }
}
