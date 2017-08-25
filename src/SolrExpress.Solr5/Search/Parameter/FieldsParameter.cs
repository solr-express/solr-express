using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Search.Parameter
{
    [FieldMustBeStoredTrue]
    public sealed class FieldsParameter<TDocument> : IFieldsParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public FieldsParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this.ExpressionBuilder = expressionBuilder;
        }

        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>>[] FieldExpressions { get; set; }

        public void AddResultInContainer(JObject container)
        {
            container.Add(this._result);
        }

        public void Execute()
        {
            var jArray = new JArray();

            foreach (var expression in this.FieldExpressions)
            {
                var value = this.ExpressionBuilder.GetFieldName(expression);

                jArray.Add(value);
            }

            this._result = new JProperty("fields", jArray);
        }
    }
}
