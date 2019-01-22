using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Utility;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class FilterParameter<TDocument> : BaseFilterParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

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
