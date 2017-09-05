using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class CursorMarkParameter : ICursorMarkParameter, ISearchItemExecution<JObject>
    {
        private JProperty _result;

        public string CursorMark { get; set; }

        public void AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        public void Execute()
        {
            this._result = new JProperty("cursorMark", this.CursorMark);
        }
    }
}
