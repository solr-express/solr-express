using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class OffsetParameter<TDocument> : IOffsetParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        long IOffsetParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IOffsetParameter<TDocument>)this;
            this._result = new JProperty("offset", parameter.Value);
        }
    }
}
