using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class SortParameter<TDocument> : BaseSortParameter<TDocument>, ISearchParameter<JObject>
        where TDocument : IDocument
    {
        public SortParameter(IExpressionBuilder<TDocument> expressionBuilder) : base(expressionBuilder)
        {
        }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var fieldName = this._expressionBuilder.GetFieldNameFromExpression(this.Expression);

            var jValue = (JValue)jObject["sort"] ?? new JValue((string)null);

            var value = $"{fieldName} {(this.Ascendent ? "asc" : "desc")}";

            if (jValue.Value != null)
            {
                jValue.Value += $", {value}";
            }
            else
            {
                jValue.Value = value;
            }

            jObject["sort"] = jValue;
        }
    }
}
