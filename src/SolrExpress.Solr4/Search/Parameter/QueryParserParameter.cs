using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class QueryParserParameter<TDocument> : IQueryParserParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

        QueryParserType IQueryParserParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = (IQueryParserParameter<TDocument>)this;
            this._result = $"defType={parameter.Value.ToString().ToLower()}";
        }
    }
}
