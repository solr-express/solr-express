using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class DefaultFieldParameter<TDocument> : BaseDefaultFieldParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public DefaultFieldParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this.ExpressionBuilder = expressionBuilder;
        }

        public void AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        public void Execute()
        {
            var fieldName = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
            this._result = new JProperty("df", fieldName);
        }
    }
}
