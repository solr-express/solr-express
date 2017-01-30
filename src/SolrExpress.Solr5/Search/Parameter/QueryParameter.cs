using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using System;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class QueryParameter<TDocument> : IQueryParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        bool ISearchParameter.AllowMultipleInstances { get; set; }

        ISearchQuery<TDocument> IQueryParameter<TDocument>.Value { get; set; }

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
