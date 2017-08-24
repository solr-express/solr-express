using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    [AllowMultipleInstances]
    [UseAnyThanSpecificParameterRather]
    public sealed class AnyParameter : IAnyParameter, ISearchItemExecution<List<string>>
    {
        private string _result;

        string IAnyParameter.Name { get; set; }

        string IAnyParameter.Value { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = (IAnyParameter)this;

            this._result = $"{parameter.Name}={parameter.Value}";
        }
    }
}
