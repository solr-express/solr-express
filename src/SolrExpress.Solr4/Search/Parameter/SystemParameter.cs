using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr4.Search.Parameter
{
    /// <summary>
    /// Internal use
    /// </summary>
    internal class SystemParameter : BaseSystemParameter, ISearchParameter<List<string>>
    {
        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            this.Parameters.Add("fl", "*,score");
            this.Parameters.Add("sort", "score desc");
            this.Parameters.Add("q", "*:*");

            foreach (var parameter in this.Parameters)
            {
                if (!container.Any(q => q.StartsWith($"{parameter.Key}=")))
                {
                    container.Add($"{parameter.Key}={parameter.Value}");
                }
            }
        }
    }
}
