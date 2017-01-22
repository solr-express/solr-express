namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in parameter validation
    /// </summary>
    public interface IValidation
    {
        /// <summary>
        /// Check for parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">Error message, if applicable</param>
        void Validate(out bool isValid, out string errorMessage);
    }
}
