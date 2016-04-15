namespace SolrExpress.Solr4.Extension.Internal
{
    /// <summary>
    /// Extension class used in Expression<Func<TDocument>> to manipulate string[]
    /// Exclusive methods to Solr 4
    /// </summary>
    internal static class ExcludesExtension
    {
        /// <summary>
        /// Get the field with excludes
        /// </summary>
        /// <param name="aliasName">Alias name</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="sortName">List of excludes</param>
        internal static string GetSolrFacetWithExcludes(this string[] excludes, string aliasName, string fieldName)
        {
            if (excludes != null && excludes.Length > 0)
            {
                return string.Format(
                    "{{!ex={0} key={1}}}{2}",
                    string.Join(",", excludes),
                    aliasName,
                    fieldName);
            }

            return string.Format("{{!key={0}}}{1}", aliasName, fieldName);
        }
    }
}