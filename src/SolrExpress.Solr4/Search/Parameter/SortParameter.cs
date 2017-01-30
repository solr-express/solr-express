using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class SortParameter<TDocument> : ISortParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        bool ISearchParameter.AllowMultipleInstances { get; set; }

        bool ISortParameter<TDocument>.Ascendent { get; set; }

        Expression<Func<TDocument, object>> ISortParameter<TDocument>.FieldExpression { get; set; }

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
