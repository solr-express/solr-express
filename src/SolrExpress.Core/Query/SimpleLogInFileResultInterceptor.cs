using System;
using System.IO;
using System.Text;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Simple solr query interceptor used to log queries
    /// </summary>
    public class SimpleLogInFileResultInterceptor : IResultInterceptor
    {
        /// <summary>
        /// Path to save log files
        /// </summary>
        private readonly string _logPath;

        /// <summary>
        /// Default constructor of class
        /// </summary>
        /// <param name="logPath">Path to save log files</param>
        public SimpleLogInFileResultInterceptor(string logPath)
        {
            Checker.IsNullOrWhiteSpace(logPath);

            this._logPath = logPath;
        }

        /// <summary>
        /// Get log content
        /// </summary>
        /// <returns>Log content</returns>
        private string GetLogContent()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"[SimpleLogQueryInterceptor] {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.zzz")}");
            sb.AppendLine(this.Json);
            sb.AppendLine(new string('-', 50));

            return sb.ToString();
        }

        /// <summary>
        /// Log information in file
        /// </summary>
        private void LogInFile(string logContent)
        {
            var directory = Path.GetDirectoryName(this._logPath);

            Directory.CreateDirectory(directory);

            var fileName = $"SimpleLogQueryInterceptor - {DateTime.Now.ToString("yyyy-MM-dd")}.txt";
            var filePath = Path.Combine(directory, fileName);

            File.AppendAllText(filePath, logContent);
        }

        /// <summary>
        /// Execute the interception
        /// </summary>
        /// <param name="json">Json to intercept</param>
        public void Execute(ref string json)
        {
            this.Json = json;

            var logContent = this.GetLogContent();

            this.LogInFile(logContent);
        }

        /// <summary>
        /// Intercepted json
        /// </summary>
        public string Json { get; private set; }
    }
}