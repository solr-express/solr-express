using System;
using System.Linq.Expressions;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using Newtonsoft.Json.Linq;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class FacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        bool ISearchParameter.AllowMultipleInstances { get; set; }

        string[] IFacetFieldParameter<TDocument>.Excludes { get; set; }

        Expression<Func<TDocument, object>> IFacetFieldParameter<TDocument>.FieldExpression { get; set; }

        int IFacetFieldParameter<TDocument>.Limit { get; set; }

        int IFacetFieldParameter<TDocument>.Minimum { get; set; }

        FacetSortType IFacetFieldParameter<TDocument>.SortType { get; set; }

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
