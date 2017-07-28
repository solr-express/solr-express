using SolrExpress.Search.Parameter;
using Newtonsoft.Json.Linq;
using SolrExpress.Search;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class MinimumShouldMatchParameter<TDocument> : IMinimumShouldMatchParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        string IMinimumShouldMatchParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IMinimumShouldMatchParameter<TDocument>)this;
            this._result = new JProperty("mm", parameter.Value);
        }
    }
}
