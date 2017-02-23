using SolrExpress.Search.Parameter;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Utility;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class FieldsParameter<TDocument> : IFieldsParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        private readonly ExpressionBuilder<TDocument> _expressionBuilder;
        private JProperty _result;

        public FieldsParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        Expression<Func<TDocument, object>>[] IFieldsParameter<TDocument>.FieldExpressions { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IFieldsParameter<TDocument>)this;
            var jArray = new JArray();

            foreach (var expression in parameter.FieldExpressions)
            {
                var value = this._expressionBuilder.GetFieldNameFromExpression(expression);

                jArray.Add(value);
            }

            this._result = new JProperty("fields", jArray);
        }
    }
}
