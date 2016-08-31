using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter.Internal;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class SortParameter<TDocument> : BaseSortParameter<TDocument>, ISearchParameter<List<string>>
        where TDocument : IDocument
    {
        private SortCommand _sortCommand;

        public SortParameter(SortCommand sortCommand)
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
            var fieldName = this.Expression.GetFieldNameFromExpression();

            this._sortCommand.Execute(fieldName, this.Ascendent, container);
        }
    }
}
