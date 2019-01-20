using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    [AllowMultipleInstances]
    public sealed class LocalParameter<TDocument> : BaseLocalParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

        public void AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        public void Execute()
        {
            Checker.IsTrue<ArgumentException>(this.Query == null && string.IsNullOrWhiteSpace(this.Value));

            var value = this.Query?.Execute() ?? this.Value;

            this._result = $"{this.Name}={value}";
        }
    }
}
