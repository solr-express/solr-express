using Newtonsoft.Json;
using SolrExpress.Search;
using SolrExpress.Search.Behaviour;
using SolrExpress.Search.Interceptor;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SolrExpress.Solr4.Search
{
    /// <summary>
    /// Parameter collection especific to SOLR 4
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

            Parallel.ForEach(searchParameters, item => ((ISearchItemExecution<List<string>>)item).Execute());

            var container = new List<string>();
            searchParameters.ForEach(q => ((ISearchItemExecution<List<string>>)q).AddResultInContainer(container));

            var json = this._solrConnection.Get(requestHandler, container);

            resultInterceptors.ForEach(q => q.Execute(requestHandler, ref json));

            return new JsonTextReader(new StringReader(json));
        }
    }
}
