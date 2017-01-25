namespace SolrExpress.Search
{
    /// <summary>
    /// Boost's type of boost calculation
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
