using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class WriteTypeParameter<TDocument> : BaseWriteTypeParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public void AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        public void Execute()
        {
            this._result = new JProperty("wt", this.Value.ToString().ToLower());
        }
    }
}
