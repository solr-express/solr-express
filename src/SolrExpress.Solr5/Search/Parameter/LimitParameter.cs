using SolrExpress.Search.Parameter;
using Newtonsoft.Json.Linq;
using SolrExpress.Search;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class LimitParameter<TDocument> : ILimitParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        long ILimitParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (ILimitParameter<TDocument>)this;
            this._result = new JProperty("limit", parameter.Value);
        }
    }
}
