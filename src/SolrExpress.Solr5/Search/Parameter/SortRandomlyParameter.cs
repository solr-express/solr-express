using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class SortRandomlyParameter<TDocument> : ISortRandomlyParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        private JProperty _result;

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            this._result = new JProperty("sort", "random");
        }
    }
}
