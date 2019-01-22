using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class BoostParameter<TDocument> : BaseBoostParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

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
