namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in solr parameter validation
    /// </summary>
    public interface IValidation
    {
        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        void Validate(out bool isValid, out string errorMessage);
    }
}
