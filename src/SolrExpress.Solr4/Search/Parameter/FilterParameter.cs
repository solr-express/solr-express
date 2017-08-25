using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class FilterParameter<TDocument> : IFilterParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

        public SearchQuery<TDocument> Query { get; set; }
        public string TagName { get; set; }

        public void AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        public void Execute()
        {
            var expression = ParameterUtil.GetFilterWithTag(this.Query.Execute(), this.TagName);

            this._result = $"fq={expression}";
        }
    }
}
