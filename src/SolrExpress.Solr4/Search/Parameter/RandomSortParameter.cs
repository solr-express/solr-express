using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter.Internal;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class RandomSortParameter : BaseRandomSortParameter, ISearchParameter<List<string>>
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
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            this._sortCommand.Execute("random", this.Ascendent, container);
        }
    }
}
