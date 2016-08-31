using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Solr5.Search.Parameter.Internal;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class RandomSortParameter : BaseRandomSortParameter, ISearchParameter<JObject>
    {
        private SortCommand _sortCommand;

        public RandomSortParameter(SortCommand sortCommand)
            : base()
        {
            this._sortCommand = sortCommand;
        }
        
        /// <summary>
        /// Execute creation of parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            this._sortCommand.Execute("random", this.Ascendent, jObject);
        }
    }
}
