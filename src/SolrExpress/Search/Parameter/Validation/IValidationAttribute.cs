namespace SolrExpress.Search.Parameter.Validation
{
    /// <summary>
    /// Validate parameters
    /// </summary>
    public interface IValidationAttribute
    {
        /// <summary>
        /// Verify if informed parameter is valid
        /// </summary>
        /// <param name="searchParameter">Parameter to verifify</param>
        /// <param name="errorMessage">Detailed error message</param>
        /// <returns>True if parameter is valid, otherwise false</returns>
        bool IsValid<TDocument>(ISearchParameter searchParameter, out string errorMessage)
            where TDocument : Document;
    }
}
