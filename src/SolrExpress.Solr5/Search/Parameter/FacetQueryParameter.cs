using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using System;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class FacetQueryParameter<TDocument> : IFacetQueryParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        string IFacetQueryParameter<TDocument>.AliasName { get; set; }

        bool ISearchParameter.AllowMultipleInstances { get; set; }

        string[] IFacetQueryParameter<TDocument>.Excludes { get; set; }

        ISearchQuery<TDocument> IFacetQueryParameter<TDocument>.Query { get; set; }

        FacetSortType IFacetQueryParameter<TDocument>.SortType { get; set; }

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
