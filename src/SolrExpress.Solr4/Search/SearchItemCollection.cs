using Newtonsoft.Json;
using SolrExpress.Search;
using SolrExpress.Search.Behaviour;
using SolrExpress.Search.Interceptor;
using SolrExpress.Search.Parameter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SolrExpress.Solr4.Search
{
    /// <summary>
    /// Parameter collection especific to SOLR 4
    /// </summary>
    public class SearchItemCollection<TDocument> : ISearchItemCollection<TDocument>
        where TDocument : IDocument
    {
        private readonly List<ISearchItem> _searchItems = new List<ISearchItem>();
        private readonly SolrConnection _solrConnection;

        public SearchItemCollection(SolrConnection solrConnection)
        {
            this._solrConnection = solrConnection;
        }

        /// <summary>
        /// Get items from internal search items list filtered by indicated type
        /// </summary>
        /// <typeparam name="T">Type to filter</typeparam>
        /// <returns>Filtered search items</returns>
        private List<T> GetItems<T>()
            where T : class
        {
            return this
                ._searchItems
                .Select(q => q as T).Where(q => q != null)
                .ToList();
        }

        void ISearchItemCollection<TDocument>.Add(ISearchItem item)
        {
            this._searchItems.Add(item);
        }

        bool ISearchItemCollection<TDocument>.Contains(Type searchItemType)
        {
            return this._searchItems.Any(q => q.GetType().Equals(searchItemType));
        }

        bool ISearchItemCollection<TDocument>.Contains<ISearchItem>()
        {
            return this._searchItems.Any(q => q is ISearchItem);
        }

        JsonReader ISearchItemCollection<TDocument>.Execute(string requestHandler)
        {
            var container = new Dictionary<string, string>();

            var changeBehaviours = this.GetItems<IChangeBehaviour>();
            var searchParameters = this.GetItems<ISearchParameter>();
            var resultInterceptors = this.GetItems<IResultInterceptor>();

            changeBehaviours.ForEach(q => q.Execute());

            Parallel.ForEach(searchParameters, item => ((ISearchItemExecution<List<string>>)item).Execute());
            searchParameters.ForEach(q => ((ISearchItemExecution<Dictionary<string, string>>)q).AddResultInContainer(container));

            var json = this._solrConnection.Get(requestHandler, container);

            resultInterceptors.ForEach(q => q.Execute(requestHandler, ref json));

            return new JsonTextReader(new StringReader(json));
        }

        List<ISearchParameter> ISearchItemCollection<TDocument>.GetParameters()
        {
            return this.GetItems<ISearchParameter>();
        }
    }
}
