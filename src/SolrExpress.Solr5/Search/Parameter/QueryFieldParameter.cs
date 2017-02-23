using SolrExpress.Search.Parameter;
using Newtonsoft.Json.Linq;
using SolrExpress.Search;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class QueryFieldParameter<TDocument> : IQueryFieldParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        private JProperty _result;

        string IQueryFieldParameter<TDocument>.Expression { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IQueryFieldParameter<TDocument>)this;
            this._result = new JProperty("qf", parameter.Expression);
        }
    }
}
