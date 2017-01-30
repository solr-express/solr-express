using System;
using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class FilterParameter<TDocument> : IFilterParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        bool ISearchParameter.AllowMultipleInstances { get; set; }

        ISearchQuery<TDocument> IFilterParameter<TDocument>.Query { get; set; }

        string IFilterParameter<TDocument>.TagName { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            throw new NotImplementedException();
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            throw new NotImplementedException();
        }
    }
}
