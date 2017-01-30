using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class FacetRangeParameter<TDocument> : IFacetRangeParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        string IFacetRangeParameter<TDocument>.AliasName { get; set; }

        bool ISearchParameter.AllowMultipleInstances { get; set; }

        bool IFacetRangeParameter<TDocument>.CountAfter { get; set; }

        bool IFacetRangeParameter<TDocument>.CountBefore { get; set; }

        string IFacetRangeParameter<TDocument>.End { get; set; }

        string[] IFacetRangeParameter<TDocument>.Excludes { get; set; }

        Expression<Func<TDocument, object>> IFacetRangeParameter<TDocument>.FieldExpression { get; set; }

        string IFacetRangeParameter<TDocument>.Gap { get; set; }

        FacetSortType IFacetRangeParameter<TDocument>.SortType { get; set; }

        string IFacetRangeParameter<TDocument>.Start { get; set; }

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