using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search.ParameterValue
{
    /// <summary>
    /// Result negative form (NOT) value parameter
    /// </summary>
    public sealed class Negate<TDocument> : ISearchParameterValue<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Create a negative form (NOT) from informed parameter value
        /// </summary>
        /// <param name="value">Paramater value used to created negative form</param>
        public Negate(ISearchParameterValue<TDocument> value)
        {
            Checker.IsNull(value);

            this.Value = value;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute()
        {
            Value.ExpressionBuilder = this.ExpressionBuilder;

            return $"-{Value.Execute()}";
        }

        /// <summary>
        /// Paramater value used to created negative form
        /// </summary>
        public ISearchParameterValue<TDocument> Value { get; private set; }

        /// <summary>
        /// Expressions builder
        /// </summary>
        public IExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
    }
}