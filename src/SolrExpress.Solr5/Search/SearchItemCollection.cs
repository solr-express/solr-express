using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Behaviour;
using SolrExpress.Search.Interceptor;
using SolrExpress.Search.Parameter;
using System.IO;
using System.Threading.Tasks;

namespace SolrExpress.Solr5.Search
{
    /// <summary>
    /// Parameter collection especific to SOLR 5
    /// </summary>
    public sealed class SearchItemCollection<TDocument> : BaseSearchItemCollection<TDocument>
        where TDocument : Document
    {
        private readonly ISolrConnection _solrConnection;

        public SearchItemCollection(ISolrConnection solrConnection)
        {
            this._solrConnection = solrConnection;
        }

        public override JsonReader Execute(string requestHandler)
        {
            var changeBehaviours = this.GetItems<IChangeBehaviour>();
            var searchParameters = this.GetItems<ISearchParameter>();
            var resultInterceptors = this.GetItems<IResultInterceptor>();

            changeBehaviours.ForEach(q => q.Execute());

            Parallel.ForEach(searchParameters, item => ((ISearchItemExecution<JObject>)item).Execute());

            var container = new JObject();
            searchParameters.ForEach(q => ((ISearchItemExecution<JObject>)q).AddResultInContainer(container));

            var json = this._solrConnection.Post(requestHandler, container);

            resultInterceptors.ForEach(q => q.Execute(requestHandler, ref json));

            return new JsonTextReader(new StringReader(json));
        }
    }
}
