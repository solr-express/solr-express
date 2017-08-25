using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class BoostParameter<TDocument> : IBoostParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

        public BoostFunctionType BoostFunctionType { get; set; }
        public SearchQuery<TDocument> Query { get; set; }

        public void AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        public void Execute()
        {
            var boostFunction = this.BoostFunctionType.ToString().ToLower();
            this._result = $"{boostFunction}={this.Query.Execute()}";
        }
    }
}
