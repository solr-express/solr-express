namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseFacetLimitParameter : IFacetLimitParameter
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Value of limit
        /// </summary>
        public int Value { get; private set; }
        
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        public IFacetLimitParameter Configure(int value)
        {
            this.Value = value;

            return this;
        }
    }
}
