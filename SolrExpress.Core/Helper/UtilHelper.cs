using SolrExpress.Core.Attribute;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Enumerator;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Core.Helper
{
    /// <summary>
    /// Helper class use in the SOLR Query core
    /// </summary>
    internal static class UtilHelper
    {
        

        
        






        /// <summary>
        /// Get the field with excludes
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="sortName">List of excludes</param>
        internal static string GetSolrFacetWithExcludesSolr5(string fieldName, string[] excludes)
        {
            if (excludes != null && excludes.Length > 0)
            {
                return string.Format(
                    "{{!ex={0}}}{1}",
                    string.Join(",", excludes),
                    fieldName);
            }

            return fieldName;
        }




    }
}
