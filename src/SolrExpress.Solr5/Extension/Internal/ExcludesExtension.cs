using System;

namespace SolrExpress.Solr5.Extension.Internal
{
    /// <summary>
    /// Extension class used in Expression<Func<TDocument>> to manipulate string[]
    /// Exclusive methods to Solr 5
    /// </summary>
    [Obsolete("Change to utility class")]
    internal static class ExcludesExtension
    {
        /// <summary>
        /// Get the field with excludes
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="sortName">List of excludes</param>
        internal static string GetSolrFacetWithExcludes(this string[] excludes, string fieldName)
        {
            if (excludes != null && excludes.Length > 0)
            {
                return $"{{!ex={string.Join(",", excludes)}}}{fieldName}";
            }

            return fieldName;
        }
    }
}
