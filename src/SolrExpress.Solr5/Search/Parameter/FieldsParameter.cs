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
    public class FieldsParameter<TDocument> : IFieldsParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public FieldsParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchItemFieldExpressions<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        ExpressionBuilder<TDocument> ISearchItemFieldExpressions<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>>[] ISearchItemFieldExpressions<TDocument>.FieldExpressions { get; set; }

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
                var value = ((IFieldsParameter<TDocument>)this).ExpressionBuilder.GetFieldName(expression);

                jArray.Add(value);
            }

            this._result = new JProperty("fields", jArray);
        }
    }
}
