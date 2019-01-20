using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    internal class SystemParameter<TDocument> : BaseSystemParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private readonly List<string> _result = new List<string>();

        public void AddResultInContainer(List<string> container)
        {
            container.AddRange(this._result);
        }

        public void Execute()
        {
            this._result.Add("echoParams=none");
            this._result.Add("indent=off");
        }
    }
}
