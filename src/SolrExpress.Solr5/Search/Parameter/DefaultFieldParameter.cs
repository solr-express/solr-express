using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class DefaultFieldParameter<TDocument> : IDefaultFieldParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public DefaultFieldParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        ExpressionBuilder<TDocument> ISearchItemFieldExpression<TDocument>.ExpressionBuilder { get; set; }
        Expression<Func<TDocument, object>> ISearchItemFieldExpression<TDocument>.FieldExpression { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IDefaultFieldParameter<TDocument>)this;
            var fieldName = parameter.ExpressionBuilder.GetFieldName(parameter.FieldExpression);
            this._result = new JProperty("df", fieldName);
        }
    }
}
