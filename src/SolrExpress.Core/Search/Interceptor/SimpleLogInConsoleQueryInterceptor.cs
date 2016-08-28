using System;
using System.Text;
using System.Diagnostics;

namespace SolrExpress.Core.Search.Interceptor
{
    /// <summary>
    /// Simple solr query interceptor used to log queries
    /// </summary>
    public class SimpleLogInConsoleQueryInterceptor : ISearchInterceptor
    {
        /// <summary>
        /// Get log content
        /// </summary>
        /// <returns>Log content</returns>
        private string GetLogContent()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"[SimpleLogQueryInterceptor] {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.zzz")}");
            sb.AppendLine(this.Query);
            sb.AppendLine(new string('-', 50));

            return sb.ToString();
        }
        
        /// <summary>
        /// Execute the interception
        /// </summary>
        /// <param name="query">Query to intercept</param>
        public void Execute(ref string query)
        {
            this.Query = query;

            var logContent = this.GetLogContent();

            Console.WriteLine(logContent);
            Debug.WriteLine(logContent, "SolrExpress");
        }

        /// <summary>
        /// Intercepted query
        /// </summary>
        public string Query { get; private set; }
    }
}