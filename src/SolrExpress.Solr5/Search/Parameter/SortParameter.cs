using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    [FieldMustBeIndexedTrue]
    public class SortParameter<TDocument> : ISortParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        private readonly ExpressionBuilder<TDocument> _expressionBuilder;
        private string _result;

        public SortParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        bool ISortParameter<TDocument>.Ascendent { get; set; }

        Expression<Func<TDocument, object>> ISearchParameterFieldExpression<TDocument>.FieldExpression { get; set; }

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

            var fieldName = this._expressionBuilder.GetFieldNameFromExpression(parameter.FieldExpression);
            this._result = $"{fieldName} {(parameter.Ascendent ? "asc" : "desc")}";
        }
    }
}
