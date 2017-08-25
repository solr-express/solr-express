using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    [UseAnyThanSpecificParameterRather]
    public sealed class AnyParameter : IAnyParameter, ISearchItemExecution<JObject>
    {
        private JProperty _result;

        public string Name { get; set; }
        public string Value { get; set; }

        public void AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        public void Execute()
        {
            this._result = new JProperty(this.Name, this.Value);
        }
    }
}
