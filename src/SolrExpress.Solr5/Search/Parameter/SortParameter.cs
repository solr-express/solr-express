using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    [FieldMustBeIndexedTrue]
    public class SortParameter<TDocument> : ISortParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private string _result;

        public SortParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        bool ISortParameter<TDocument>.Ascendent { get; set; }

        ExpressionBuilder<TDocument> ISearchItemFieldExpression<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>> ISearchItemFieldExpression<TDocument>.FieldExpression { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jValue = (JValue)container["sort"] ?? new JValue((string)null);

            if (jValue.Value != null)
            {
                jValue.Value += $", {this._result}";
            }
            else
            {
                jValue.Value = this._result;
            }

            container["sort"] = jValue;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (ISortParameter<TDocument>)this;

            var fieldName = ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder.GetFieldName(parameter.FieldExpression);
            this._result = $"{fieldName} {(parameter.Ascendent ? "asc" : "desc")}";
        }
    }
}
