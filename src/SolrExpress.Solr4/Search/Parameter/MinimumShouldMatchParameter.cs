using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class MinimumShouldMatchParameter<TDocument> : IMinimumShouldMatchParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        private string _result;

        bool ISearchParameter.AllowMultipleInstances { get; set; }

        string IMinimumShouldMatchParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = ((IMinimumShouldMatchParameter<TDocument>)this);

            this._result = $"mm={parameter.Value}";
        }
    }
}
