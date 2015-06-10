namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Base interface used to parse the SOLR results when a json string is necessary
    /// </summary>
    public interface IConvertJsonPlainText
    {
        /// <summary>
        /// Execute the parse of the JSON string
        /// </summary>
        /// <param name="jsonPlainText">JSON string used in the parse</param>
        void Execute(string jsonPlainText);
    }
}
