using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Solr5.Search.Parameter.Internal;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class SortParameter<TDocument> : BaseSortParameter<TDocument>, ISearchParameter<JObject>
        where TDocument : IDocument
    {
        private SortCommand _sortCommand;

        public SortParameter(SortCommand sortCommand)
            : base()
        {
            this._sortCommand = sortCommand;
        }
        
        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var fieldName = this.Expression.GetFieldNameFromExpression();

            this._sortCommand.Execute(fieldName, this.Ascendent, jObject);
        }
    }
}
