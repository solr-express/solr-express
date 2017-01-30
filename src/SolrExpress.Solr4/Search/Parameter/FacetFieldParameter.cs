using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class FacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        bool ISearchParameter.AllowMultipleInstances { get; set; }

        string[] IFacetFieldParameter<TDocument>.Excludes { get; set; }

        Expression<Func<TDocument, object>> IFacetFieldParameter<TDocument>.FieldExpression { get; set; }

        int IFacetFieldParameter<TDocument>.Limit { get; set; }

        int IFacetFieldParameter<TDocument>.Minimum { get; set; }

        FacetSortType IFacetFieldParameter<TDocument>.SortType { get; set; }

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
