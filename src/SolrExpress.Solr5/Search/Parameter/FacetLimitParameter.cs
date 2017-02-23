using SolrExpress.Search.Parameter;
using Newtonsoft.Json.Linq;
using SolrExpress.Search;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class FacetLimitParameter<TDocument> : IFacetLimitParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        private JProperty _result;

        long IFacetLimitParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IFacetLimitParameter<TDocument>)this;
            this._result = new JProperty("facet.limit", parameter.Value);
        }
    }
}