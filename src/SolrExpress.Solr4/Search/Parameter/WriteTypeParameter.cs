using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class WriteTypeParameter<TDocument> : IWriteTypeParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

        WriteType IWriteTypeParameter<TDocument>.Value { get;set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = (IWriteTypeParameter<TDocument>)this;
            this._result = $"wt={parameter.Value.ToString().ToLower()}";
        }
    }
}
