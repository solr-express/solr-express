using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class StandardQueryParameter<TDocument> : IStandardQueryParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

        public SearchQuery<TDocument> Value { get;set; }

        public void AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        public void Execute()
        {
            this._result = $"q.alt={this.Value.Execute()}";
        }
    }
}
