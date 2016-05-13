namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Boost's type of the boost calculation
    /// </summary>
    public enum BoostFunctionType
    {
        /// <summary>
        /// Sort using "boost" function (multiplicative)
        /// </summary>
        Boost,

        /// <summary>
        /// Sort using "bf" function (additive)
        /// </summary>
        Bf
    }
}
