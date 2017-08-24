using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class FacetLimitParameter<TDocument> : IFacetLimitParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;
        
        long IFacetLimitParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = (IFacetLimitParameter<TDocument>)this;

            this._result = $"facet.limit={parameter.Value}";
        }
    }
}