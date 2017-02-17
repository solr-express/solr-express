using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class BoostParameter<TDocument> : IBoostParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        private string _result;

        BoostFunctionType IBoostParameter<TDocument>.BoostFunctionType { get; set; }

        ISearchQuery<TDocument> IBoostParameter<TDocument>.Query { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = ((IBoostParameter<TDocument>)this);
            var boostFunction = parameter.BoostFunctionType.ToString().ToLower();

            this._result = $"{boostFunction}={parameter.Query.Execute()}";
        }
    }
}
