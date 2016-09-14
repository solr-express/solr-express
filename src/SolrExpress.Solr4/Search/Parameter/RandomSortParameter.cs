using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class RandomSortParameter : BaseRandomSortParameter, ISearchParameter<List<string>>
    {
        /// <summary>
        /// Execute creation of parameter "sort"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            var fieldName = "random";

            var value = $"{fieldName} {(this.Ascendent ? "asc" : "desc")}";

            var sort = container.FirstOrDefault(q => q.StartsWith("sort="));

            if (!string.IsNullOrWhiteSpace(sort))
            {
                container.Remove(sort);

                sort = $"{sort},{value}";
            }
            else
            {
                sort = $"sort={value}";
            }

            container.Add(sort);
        }
    }
}
