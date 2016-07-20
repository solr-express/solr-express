namespace SolrExpress.Core.Query.ParameterValue
{
    /// <summary>
    /// Result negative form (NOT) value parameter
    /// </summary>
    public sealed class Negate : IQueryParameterValue
    {
        /// <summary>
        /// Create a negative form (NOT) from informed parameter value
        /// </summary>
        /// <param name="value">Paramater value used to created negative form</param>
        public Negate(IQueryParameterValue value)
        {
            Checker.IsNull(value);

            this.Value = value;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute() => $"-{Value.Execute()}";

        /// <summary>
        /// Paramater value used to created negative form
        /// </summary>
        public IQueryParameterValue Value { get; private set; }
    }
}