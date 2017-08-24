using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class QueryParserParameter<TDocument> : IQueryParserParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        QueryParserType IQueryParserParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IQueryParserParameter<TDocument>)this;
            this._result = new JProperty("defType", parameter.Value.ToString().ToLower());
        }
    }
}
