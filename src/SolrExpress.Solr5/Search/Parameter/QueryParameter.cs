using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using Newtonsoft.Json.Linq;
using SolrExpress.Search;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class QueryParameter<TDocument> : IQueryParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        SearchQuery IQueryParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IQueryParameter<TDocument>)this;
            this._result = new JProperty("query", parameter.Value.Execute());
        }
    }
}
