using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class QueryParameter<TDocument> : BaseQueryParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public void AddResultInContainer(JObject container)
        {
            container.Add(this._result);
        }

        public void Execute()
        {
            this._result = new JProperty("query", this.Value.Execute());
        }
    }
}
