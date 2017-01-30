using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class FacetQueryParameter<TDocument> : IFacetQueryParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        string IFacetQueryParameter<TDocument>.AliasName { get; set; }

        bool ISearchParameter.AllowMultipleInstances { get; set; }

        string[] IFacetQueryParameter<TDocument>.Excludes { get; set; }

        ISearchQuery<TDocument> IFacetQueryParameter<TDocument>.Query { get; set; }

        FacetSortType IFacetQueryParameter<TDocument>.SortType { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            throw new NotImplementedException();
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            throw new NotImplementedException();
        }
    }
}
