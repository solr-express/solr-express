using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Behaviour;
using SolrExpress.Search.Interceptor;
using SolrExpress.Search.Parameter;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SolrExpress.Solr5.Search
{
    /// <summary>
    /// Parameter collection especific to SOLR 5
    /// </summary>
    public sealed class SearchItemCollection<TDocument> : BaseSearchItemCollection<TDocument>
        where TDocument : Document
    {
        private readonly ISolrConnection<TDocument> _solrConnection;

        public SearchItemCollection(ISolrConnection<TDocument> solrConnection)
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

            if (resultInterceptors.Any())
            {
                var jsonPlainText = this._solrConnection.Post(requestHandler, container);
                resultInterceptors.ForEach(q => q.Execute(requestHandler, ref jsonPlainText));
                return new JsonTextReader(new StringReader(jsonPlainText));
            }

            var jsonStream = this._solrConnection.PostStream(requestHandler, container);
            return new JsonTextReader(new StreamReader(jsonStream));
        }
    }
}
