using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    internal class SystemParameter<TDocument> : BaseSystemParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _parameter1;
        private JProperty _parameter2;

        public void AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._parameter1);
            jObj.Add(this._parameter2);
            container["params"] = jObj;
        }

        public void Execute()
        {
            this._parameter1 = new JProperty("echoParams", "none");
            this._parameter2 = new JProperty("indent", "off");
        }
    }
}
