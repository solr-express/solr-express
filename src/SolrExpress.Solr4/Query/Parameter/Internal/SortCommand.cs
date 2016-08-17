using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr4.Query.Parameter.Internal
{
    internal class SortCommand
    {
        /// <summary>
        /// Execute creation of parameter "sort"
        /// </summary>
        /// <param name="fieldName">Field name to add in sort parameter</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="container">Container to parameters to request to SOLR</param>
        internal void Execute(string fieldName, bool ascendent , List<string> container)
        {
            var value = $"{fieldName} {(ascendent ? "asc" : "desc")}";

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
