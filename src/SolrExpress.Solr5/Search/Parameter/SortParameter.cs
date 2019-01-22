using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    [FieldMustBeIndexedTrue]
    public sealed class SortParameter<TDocument> : BaseSortParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private string _result;

        public SortParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this.ExpressionBuilder = expressionBuilder;
        }

        public void AddResultInContainer(JObject container)
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

        public void Execute()
        {
            var fieldName = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
            this._result = $"{fieldName} {(this.Ascendent ? "asc" : "desc")}";
        }
    }
}
