using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    public class AnyParameter<TDocument> : IAnyParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        private JProperty _result;

        string IAnyParameter<TDocument>.Name { get; set; }

        string IAnyParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IAnyParameter<TDocument>)this;
            this._result = new JProperty(parameter.Name, parameter.Value);
        }
    }
}
