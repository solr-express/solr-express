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

        string IAnyParameter.Name { get; set; }

        string IAnyParameter.Value { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IAnyParameter)this;
            this._result = new JProperty(parameter.Name, parameter.Value);
        }
    }
}
