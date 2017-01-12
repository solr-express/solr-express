using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class QueryParameter<TDocument> : BaseQueryParameter<TDocument>, ISearchParameterExecute<JObject>
        where TDocument : IDocument
    {
        public QueryParameter(IExpressionBuilder<TDocument> expressionBuilder)
            : base(expressionBuilder)
        {
        }

        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject["query"] = new JValue(this.Value.Execute());
        }
    }
}
