using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class StartParameter : BaseOffsetParameter, ISearchParameter<List<string>>
    {
        /// <summary>
        /// Execute the creation of the parameter "start"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add($"start={this.Value}");
        }
    }
}
