using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolrExpress.Solr5.Search
{
    /// <summary>
    /// Parameter collection
    /// </summary>
    public class SearchParameterCollection<TDocument> : ISearchParameterCollection<TDocument>
        where TDocument : IDocument
    {
        private IEnumerable<ISearchParameter> _parameters;

        void ISearchParameterCollection<TDocument>.Add(IEnumerable<ISearchParameter> parameters)
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
                        ((ISearchParameterExecute<JObject>)item).Execute(jObject);
                    }
                });
            }

            return jObject.ToString();
        }
    }
}