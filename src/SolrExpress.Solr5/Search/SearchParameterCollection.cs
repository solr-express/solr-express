using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SolrExpress.Solr5.Search
{
    /// <summary>
    /// Parameter collection
    /// </summary>
    public class SearchParameterCollection<TDocument> : ISearchParameterCollection<TDocument>
        where TDocument : IDocument
    {
        private IEnumerable<ISearchParameter<TDocument>> _parameters;

        IExpressionBuilder<TDocument> ISearchParameterCollection<TDocument>.ExpressionBuilder { get; set; }

        void ISearchParameterCollection<TDocument>.Add(IEnumerable<ISearchParameter<TDocument>> parameters)
        {
            this._parameters = parameters;
        }

        string ISearchParameterCollection<TDocument>.Execute()
        {
            var jObject = new JObject();

            if (this._parameters != null)
            {
                Parallel.ForEach(this._parameters, item =>
                {
                    lock (jObject)
                    {
                        ((ISearchParameter<TDocument, JObject>)item).ExpressionBuilder = ((ISearchParameterCollection<TDocument>)this).ExpressionBuilder;
                        ((ISearchParameter<TDocument, JObject>)item).Execute(jObject);
                    }
                });
            }

            return jObject.ToString();
        }
    }
}