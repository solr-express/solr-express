using System;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using Newtonsoft.Json.Linq;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class BoostParameter<TDocument> : IBoostParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        bool ISearchParameter.AllowMultipleInstances { get; set; }

        BoostFunctionType IBoostParameter<TDocument>.BoostFunctionType { get; set; }

        ISearchQuery<TDocument> IBoostParameter<TDocument>.Query { get; set; }

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
