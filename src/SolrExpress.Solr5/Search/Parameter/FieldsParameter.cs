using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class FieldsParameter<TDocument> : BaseFieldsParameter<TDocument>, ISearchParameter<JObject>
        where TDocument : IDocument
    {
        public FieldsParameter(IExpressionBuilder<TDocument> expressionBuilder) : base(expressionBuilder)
        {
        }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jArray = (JArray)jObject["fields"] ?? new JArray();

            foreach (var expression in this.Expressions)
            {
                var value = this._expressionBuilder.GetFieldNameFromExpression(expression);

                jArray.Add(value);
            }

            jObject["fields"] = jArray;
        }
    }
}
