using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class FilterParameter<TDocument> : IFilterParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        private string _result;
        
        SearchQuery IFilterParameter<TDocument>.Query { get; set; }

        string IFilterParameter<TDocument>.TagName { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = (IFilterParameter<TDocument>)this;
            var expression = ParameterUtil.GetFilterWithTag(parameter.Query.Execute(), parameter.TagName);

            this._result = $"fq={expression}";
        }
    }
}
