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

        SearchQuery<TDocument> IStandardQueryParameter<TDocument>.Value { get;set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = (IStandardQueryParameter<TDocument>)this;
            this._result = $"q.alt={parameter.Value.Execute()}";
        }
    }
}
